namespace BiotrananMVC.Models;
public interface IMovieService
{
    Task<List<Movie>> GetMoviesFromApi();
    Task<List<MovieView>> GetMovieViewsFromApi();
    Task<Reservation> NewReservationToApi(Reservation reservation);
}
