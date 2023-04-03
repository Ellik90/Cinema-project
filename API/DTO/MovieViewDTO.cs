using API.Models;
namespace API.DTO;
public class MovieViewDTO
{
    public int MovieViewId { get; set; }
    public DateTime Date { get; set; }
    public int MovieId { get; set; }
    public int SalonId { get; set; }
    public Movie Movie;

    public MovieViewDTO(int movieViewId, int movieId, int salonId, DateTime date)
    {
        MovieViewId = movieViewId;
        MovieId = movieId;
        SalonId = salonId;
        Date = date;
    }
    public MovieViewDTO() { }
}