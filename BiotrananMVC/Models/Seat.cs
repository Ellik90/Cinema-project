namespace BiotrananMVC.Models;
public class Seat
{
    public int SeatId { get; set; }
    public int SalonId { get; set; }
    public Salon Salon;

    public Seat(int seatId, int salonId, Salon salon)
    {
        SeatId = seatId;
        SalonId = salonId;
        Salon = salon;
    }

    public Seat(){}
}