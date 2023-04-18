using API.Models;
using Microsoft.EntityFrameworkCore;
using API.Interface;

namespace API.Data;
public class MovieRepository : IMovieRepository
{
    MyDbContext _myDbContext;
    public MovieRepository(MyDbContext myDbContext)
    {
        _myDbContext = myDbContext;
    }

    public async Task<List<Movie>> GetMovies()
    {
        try
        {
            List<Movie> movies = await _myDbContext.movies.ToListAsync();
            return movies;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public async Task<Movie> GetMovieById(int movieId)
    {
        try
        {
            List<Movie> getMovie = new();
            getMovie = await _myDbContext.movies.ToListAsync();
            return getMovie.Find(g => g.MovieId == movieId);
        }
        catch (Exception)
        {
            return null;
        }
    }

    // public async Task<List<Movie>> GetForDeleteAll()
    // {
    //     try
    //     {
    //         List<Movie> movies = await _myDbContext.movies.ToListAsync();
    //         await _myDbContext.SaveChangesAsync();
    //         return movies;
    //     }
    //     catch (Exception ex)
    //     {
    //         throw new Exception("Ett fel uppstod vid hämtning av filmer för radering.", ex);
    //     }
    // }


    public async Task<Movie> CreateMovie(Movie movie)
    {
        try
        {
            _myDbContext.movies.Add(movie);
            await _myDbContext.SaveChangesAsync();
            return movie;
        }
        catch (Exception ex)
        {
            throw new Exception("Could not create movie", ex);
        }
    }

    public async Task<Movie> UpdateMovie(Movie movie)
    {
        try
        {
            var existingMovie = await _myDbContext.movies.FindAsync(movie.MovieId);
            if (existingMovie == null)
            {
                return null;
            }
            else
            {
                await _myDbContext.SaveChangesAsync();
                return existingMovie;
            }
        }
        catch (Exception ex)
        {
            throw new Exception("Kunde inte uppdatera film", ex);
        }
    }

    public async Task<Movie> DeleteMovie(Movie movie)
    {
        try
        {
            var existingMovie = await _myDbContext.movies.FindAsync(movie.MovieId);
            if (existingMovie == null)
            {
                return null;
            }
            else
            {
                _myDbContext.movies.Remove(existingMovie);
                await _myDbContext.SaveChangesAsync();
                return existingMovie;
            }
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public async Task DeleteAll()
    {
        try
        {
            var allMovies = await _myDbContext.movies.ToListAsync();
            _myDbContext.movies.RemoveRange(allMovies);
            await _myDbContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception("Ett fel inträffade när alla filmer skulle tas bort.", ex);
        }
    }
}