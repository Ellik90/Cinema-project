namespace API.Models;
public class Salon
{
    public int SalonId { get; set; }
    public string SalonName { get; set; } // kommer inte med i view
    public int NumberOfSeats { get; set; }
    public int NumberOfRows { get; set; }
    public decimal SalonPrice { get; set; }
    public ICollection<Seat> Seats { get; set; }
   
    public ICollection<MovieView> Views;

    public Salon(string salonName, int numberOfSeats, int numberOfRows, decimal salonPrice, ICollection<Seat> seats)
    {
        SalonName = salonName;
        NumberOfSeats = numberOfSeats;
        SalonPrice = salonPrice;
        Seats = seats;
    }
    public Salon(){}
}