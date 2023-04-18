namespace BiotrananMVC.Models;
public class Salon
{
    public int SalonId { get; set; }
    public string SalonName { get; set; } 
    public int NumberOfSeats { get; set; }
    public int NumberOfRows { get; set; }   
    public ICollection<Seat> Seats { get; set; }   
    public ICollection<MovieView> Views;

    public Salon(string salonName, int numberOfSeats, int numberOfRows, ICollection<Seat> seats)
    {
        SalonName = salonName;
        NumberOfSeats = numberOfSeats;
        Seats = seats;
    }
    public Salon(){}
}