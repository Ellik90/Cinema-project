using API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Sqlite;
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
        await _myDbContext.movieViews.AddAsync(movieView);
        //  _myDbContext.movies.Include(m => m.MovieId);
        await _myDbContext.SaveChangesAsync();
        return movieView;
    }

    public async Task<List<MovieView>> GetMovieViews()
    {
        var reservation = await _myDbContext.reservations.FindAsync(1);
        if (_myDbContext.reservations.Contains(reservation))
        {
            _myDbContext.reservations.Remove(reservation);
        }

        // var reservation = await _myDbContext.reservations.FindAsync(1);
        // _myDbContext.reservations.Remove(reservation);

        List<MovieView> movieViews = new();

        movieViews = await _myDbContext.movieViews.ToListAsync();
        foreach (var view in movieViews)
        {
            var salon = await _myDbContext.salons
        .Where(s => s.SalonId == view.SalonId).FirstOrDefaultAsync();

            var availableSeats = salon.NumberOfSeats;

            var reservations = await _myDbContext.reservations.Where(r => r.MovieViewId == view.MovieViewId).ToListAsync();

            foreach (var r in reservations)
            {
                var reservedSeats = r.NumberOfSeats;
                availableSeats -= reservedSeats;
            }
            view.AvailableSeats = availableSeats;
        }

        return movieViews;

    }

    public async Task<MovieView> GetMovieViewById(int movieViewId)
    {
        try
        {
            return await _myDbContext.movieViews.FindAsync(movieViewId);
        }
        catch (Exception)
        {
            return null;
        }
    }

    public async Task<List<MovieView>> GetMovieViewsByMovieId(int movieId)
    {
        var movieViews = await _myDbContext.movieViews
            .Where(mv => mv.MovieId == movieId)
            .ToListAsync();
        return movieViews;
    }

    public async Task<List<MovieView>> GetMovieViewsByDateAndSalonId(DateTime date, int salonId)
    {
        var movieViews = await _myDbContext.movieViews.Where(mv => mv.Date == date && mv.SalonId == salonId).ToListAsync();
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
        var upcomingViews = await _myDbContext.movieViews
            .Include(v => v.Salon)
            .Include(v => v.Movie)
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
        var existingMovieView = await _myDbContext.movieViews.FindAsync(movieView.MovieViewId);
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
        var existingMovieView = await _myDbContext.movieViews.FindAsync(movieView.MovieViewId);
        if (existingMovieView == null)
        {
            throw new Exception("Movie view cant be found");
        }
        else
        {
            _myDbContext.movieViews.Remove(existingMovieView);
            await _myDbContext.SaveChangesAsync();
            return existingMovieView;
        }
    }
}