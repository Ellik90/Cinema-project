using API.Models;
namespace API.DTO;
public class MovieViewDTO
{
    public int MovieViewId { get; set; }
    public string MovieTitle { get; set; }
    public DateTime Date { get; set; }
    public int MovieId { get; set; }
    public int SalonId { get; set; }
    public string SalonName { get; set; }
    public int AvailableSeats {get;set;}

    public Movie Movie;
    public Salon Salon;

    public MovieViewDTO(int movieViewId, string movieTitle, int movieId, int salonId, string salonName, DateTime date)
    {
        MovieViewId = movieViewId;
        MovieTitle = movieTitle;
        MovieId = movieId;
        SalonId = salonId;
        SalonName = salonName;
        Date = date;
    
    }


    // public MovieViewDTO(int movieViewId, string movieTitle, int movieId, int salonId, string salonName, DateTime date)
    // {
    //     MovieViewId = movieViewId;
    //     MovieTitle = Movie.Title;
    //     MovieId = movieId;
    //     SalonId = salonId;
    //     SalonName = Salon.SalonName;  
    //     Date = date;

    // }
    public MovieViewDTO() { }
}