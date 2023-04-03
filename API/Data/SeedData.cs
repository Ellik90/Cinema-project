
using API.Models;
using Microsoft.EntityFrameworkCore;
using API.DTO;
namespace API.Data;

public class SeedData
{
    MyDbContext _myDbContext;

    public SeedData(MyDbContext myDbContext)
    {
        _myDbContext = myDbContext;
    }

    public async Task<List<Movie>> GetMovies()
    {
        List<Movie> movies = new();

        movies = await _myDbContext.movies.ToListAsync();
        return movies;

    }

    // public async Task<List<MovieDTO>> GetMovies()
    // {
    //     List<MovieDTO> movies = new();
    //     try
    //     {
    //         movies = await _myDbContext.movies.ToListAsync();
    //         await _myDbContext.SaveChangesAsync();
    //         if (movies.Count < 1)
    //         {
    //             throw new Exception();
    //         }
    //         return movies;
    //     }
    //     catch (Exception)
    //     {
    //         MovieDTO movieDTO = new();
    //         movies.Add(movieDTO);
    //         await _myDbContext.SaveChangesAsync();
    //         return movies;
    //     }
    // }

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

    //Ändra SeedData till MovieSeedData

    public async Task<List<Movie>> GetForDeleteAll()
    {
        List<Movie> movies = new();

        movies = await _myDbContext.movies.ToListAsync();
        await _myDbContext.SaveChangesAsync();
        return movies;
    }

    // public async Task<Movie> CreateMovie(Movie movie)
    // {
    //     _myDbContext.movies.Add(movie);
    //     await _myDbContext.SaveChangesAsync();
    //     return movie;
    // }

    public async Task<Movie> CreateMovie(Movie movie)
    {
        _myDbContext.movies.Add(movie);
        await _myDbContext.SaveChangesAsync();
        return movie;
    }

    public async Task<Movie> UpdateMovie(Movie movie)
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

    public async Task<Movie> DeleteMovie(Movie movie)
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

    public async Task DeleteAll()
    {
        var allMovies = await _myDbContext.movies.ToListAsync();
        _myDbContext.movies.RemoveRange(allMovies);
        await _myDbContext.SaveChangesAsync();
    }

}