namespace API.Models;

public class Seat
{
    public int Id { get; set; }
    public int SalonId { get; set; }
    public Salon salon { get; set; }
}