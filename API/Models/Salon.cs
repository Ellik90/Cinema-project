namespace API.Models;
public class Salon
{
    public int SalonId { get; set; }
    public string SalonName { get; set; }
    public int NumberOfSeats { get; set; }
    public int NumberOfRows { get; set; }
    public ICollection<Seat> Seats { get; set; }

    public Salon(string salonName, int numberOfSeats, int numberOfRows, ICollection<Seat> seats)
    {
        SalonName = salonName;
        SalonName = salonName;
        NumberOfSeats = numberOfSeats;
        NumberOfRows = numberOfRows;
        Seats = seats;
    }
    public Salon(){}
}