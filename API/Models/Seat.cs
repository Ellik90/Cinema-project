namespace API.Models;

public class Seat
{
    public int SeatId { get; set; }
    public int SalonId { get; set; }
    public Salon Salon;

    public Seat(){}
}