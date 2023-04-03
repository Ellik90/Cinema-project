using API.Models;
namespace API.DTO;
public class MovieViewDTO
{
    public int MovieviewId { get; set; }
    public DateTime date { get; set; }
    public int MovieId { get; set; }
    public int SalonId { get; set; }
    public Movie Movie { get; set; }
    
    public MovieViewDTO(int movieId, int salonId, Movie movie)
    {
        MovieId = movieId;
        SalonId = salonId;
        Movie = movie;
    }
    public MovieViewDTO(){}
}