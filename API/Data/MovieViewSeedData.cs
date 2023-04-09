using API.Models;
using Microsoft.EntityFrameworkCore;
using API.DTO;

namespace API.Data;

public class MovieViewSeedData
{
    MyDbContext _myDbContext;

    public MovieViewSeedData(MyDbContext myDbContext)
    {
        _myDbContext = myDbContext;
    }

    public async Task<MovieView> CreateNewMovieView(MovieView movieView)
    {
        await _myDbContext.views.AddAsync(movieView);
        //  _myDbContext.movies.Include(m => m.MovieId);
        await _myDbContext.SaveChangesAsync();
        return movieView;
    }

    public async Task<List<MovieView>> GetMovieViews()
    {
        List<MovieView> movieViews = new();

        movieViews = await _myDbContext.views.ToListAsync();
        return movieViews;

    }

    // public async Task<MovieView> GetMovieViewById(int movieViewId)
    // {
    //     try
    //     {
    //         List<MovieView> getMovieView = new();
    //         getMovieView = await _myDbContext.views.ToListAsync();
    //         return getMovieView.Find(g => g.MovieViewId == movieViewId);
    //     }
    //     catch (Exception)
    //     {
    //         return null;
    //     }
    // }

    public async Task<List<MovieView>> GetUpcomingMovieViews()
    {
        var upcomingViews = await _myDbContext.views
            .Include(v => v.Salon)
            .ThenInclude(s => s.Movie)
            .Where(v => v.Date >= DateTime.Now)
            .Select(v => new MovieView
            {
                MovieId = v.MovieId,
                Date = v.Date,
                SalonId = v.SalonId
            })
            .ToListAsync();

        return upcomingViews;
    }


    public async Task<MovieView> UpdateMovieView(MovieView movieView)
    {
        var existingMovieView = await _myDbContext.views.FindAsync(movieView.MovieViewId);
        if (existingMovieView == null)
        {
            throw new Exception("Movie view cant be found");
        }
        else
        {
            await _myDbContext.SaveChangesAsync();
            return existingMovieView;
        }
    }

    public async Task<MovieView> DeleteMovieById(MovieView movieView)
    {
        var existingMovieView = await _myDbContext.views.FindAsync(movieView.MovieViewId);
        if (existingMovieView == null)
        {
            throw new Exception("Movie view cant be found");
        }
        else
        {
            _myDbContext.views.Remove(existingMovieView);
            await _myDbContext.SaveChangesAsync();
            return existingMovieView;
        }
    }
}