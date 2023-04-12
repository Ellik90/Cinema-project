namespace BiotrananMVC.Models;

public class MovieView
{
    public int MovieViewId { get; set; }
    public string MovieTitle { get; set; }
    public DateTime Date { get; set; }
    public int MovieId { get; set; }
    public int SalonId { get; set; }
    public Movie Movie { get; set; }
    public Salon Salon { get; set; }
    public ICollection<Reservation> Reservations;
     public int AvailableSeats {get;set;}

    public MovieView(string movieTitle, int movieId, int salonId, int availableSeats)
    {
        MovieTitle = movieTitle;
        MovieId = movieId;
        SalonId = salonId;
        AvailableSeats = availableSeats;
        // Movie = movie;
        // Salon = salon;
    }

    public MovieView() { }
}