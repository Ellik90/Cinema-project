using API.Models;

namespace API.Interface
{
    public interface IMovieViewRepository
    {
        Task<MovieView> CreateNewMovieView(MovieView movieView);
        Task<List<MovieView>> GetMovieViews();
        Task<MovieView> GetMovieViewById(int movieViewId);
        Task<List<MovieView>> GetMovieViewsByMovieId(int movieId);
        Task<List<MovieView>> GetMovieViewsByDateAndSalonId(DateTime date, int salonId);
        Task<List<MovieView>> GetUpcomingMovieViews();
        Task<MovieView> UpdateMovieView(MovieView movieView);
        Task<MovieView> DeleteMovieViewById(MovieView movieView);
    }
}
