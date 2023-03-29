namespace API.Models;
public class Salon
{
    public int Id { get; set; }
    public int Seats { get; set; }
    public int SeatRow { get; set; }
    public ICollection<Seat> seats { get; set; }
}