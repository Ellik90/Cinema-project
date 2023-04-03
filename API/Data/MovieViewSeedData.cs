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
        return await _myDbContext.views.ToListAsync();
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