using API.Models;

namespace API.Interface
{
    public interface IMovieRepository
    {
        Task<List<Movie>> GetMovies();
        Task<Movie> GetMovieById(int movieId);
        Task<Movie> CreateMovie(Movie movie);
        Task<Movie> UpdateMovie(Movie movie);
        Task<Movie> DeleteMovie(Movie movie);
        Task DeleteAll();
    }
}

