using API.Models;
using Microsoft.EntityFrameworkCore;
using API.Interface;

namespace API.Data;
public class MovieViewRepository : IMovieViewRepository
{
    MyDbContext _myDbContext;

    public MovieViewRepository(MyDbContext myDbContext)
    {
        _myDbContext = myDbContext;
    }

    public async Task<MovieView> CreateNewMovieView(MovieView movieView)
    {
        try
        {
            await _myDbContext.movieViews.AddAsync(movieView);
            await _myDbContext.SaveChangesAsync();
            return movieView;
        }
        catch (Exception ex)
        {
            throw new Exception("Ett fel uppstod", ex);
        }
    }
    public async Task<List<MovieView>> GetMovieViews()
    {
        try
        {
            var reservation = await _myDbContext.reservations.FindAsync(1);
            if (_myDbContext.reservations.Contains(reservation))
            {
                _myDbContext.reservations.Remove(reservation);
            }

            List<MovieView> movieViews = new();

            movieViews = await _myDbContext.movieViews.ToListAsync();
            foreach (var view in movieViews)
            {
                var salon = await _myDbContext.salons.Where(s => s.SalonId == view.SalonId).FirstOrDefaultAsync();
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
        catch (Exception ex)
        {
            throw new Exception("Ett fel uppstod vid hämtning fimvisningar.", ex);
        }
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
        try
        {
            var movieViews = await _myDbContext.movieViews
                .Where(mv => mv.MovieId == movieId)
                .ToListAsync();
            return movieViews;
        }
        catch (Exception ex)
        {
            throw new Exception($"Ett fel uppstod vid hämtning av moviviewId {movieId}. Felmeddelande {ex.Message}");
        }
    }

    public async Task<List<MovieView>> GetMovieViewsByDateAndSalonId(DateTime date, int salonId)
    {
        try
        {
            var movieViews = await _myDbContext.movieViews.Where(mv => mv.Date == date && mv.SalonId == salonId).ToListAsync();
            return movieViews;
        }
        catch (Exception ex)
        {
            throw new Exception($"Ett fel uppstod vid hämtning av views: {ex.Message}");
        }
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
        try
        {
            var upcomingViews = await _myDbContext.movieViews
                .Include(v => v.Salon)
                .Include(v => v.Movie)
                .Where(v => v.Date >= DateTime.Now)
                .Select(v => new MovieView
                {
                    MovieId = v.MovieId,
                    Date = v.Date,
                    SalonId = v.SalonId,
                    Movie = v.Movie
                })
                .ToListAsync();
            return upcomingViews;
        }
        catch (Exception ex)
        {
            throw new Exception("Ett fel uppstod vid hämtning av kommande visningar.", ex);
        }
    }

    public async Task<MovieView> UpdateMovieView(MovieView movieView)
    {
        try
        {
            var existingMovieView = await _myDbContext.movieViews.FindAsync(movieView.MovieViewId);
            if (existingMovieView == null)
            {
                throw new Exception("Filmvisning hittades inte");
            }

            var salon = await _myDbContext.salons.FindAsync(movieView.SalonId);
            if (salon == null)
            {
                throw new Exception("Salong hittades inte");
            }

            var movie = await _myDbContext.movies.FindAsync(movieView.MovieId);
            if (movie == null)
            {
                throw new Exception("Film hittades inte");
            }

            existingMovieView.Date = movieView.Date;
            existingMovieView.Movie = movie;
            existingMovieView.Salon = salon;
            await _myDbContext.SaveChangesAsync();
            return existingMovieView;
        }
        catch (Exception ex)
        {
            throw new Exception("Ett fel uppstod vid uppdatering", ex);
        }
    }

    public async Task<MovieView> DeleteMovieViewById(MovieView movieView)
    {
        try
        {
            var existingMovieView = await _myDbContext.movieViews.FindAsync(movieView.MovieViewId);
            if (existingMovieView == null)
            {
                throw new ArgumentException("Filmvisningen hittades inte");
            }
            _myDbContext.movieViews.Remove(existingMovieView);
            await _myDbContext.SaveChangesAsync();
            return existingMovieView;
        }
        catch (Exception ex)
        {
            throw new Exception("Ett fel uppstod vid borttagning av film", ex);
        }
    }
}