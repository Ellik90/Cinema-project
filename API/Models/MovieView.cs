namespace API.Models;

public class MovieView
{
    public int MovieViewId { get; set; }
    public DateTime Date { get; set; }
    public int MovieId { get; set; }
    public int SalonId { get; set; }
    public Movie Movie { get; set; }
    public Salon Salon { get; set; }
    
    public MovieView(int movieId, int salonId, Movie movie)
    {
        MovieId = movieId;
        SalonId = salonId; 
        Movie = movie;   
    }
    public MovieView(){}
}