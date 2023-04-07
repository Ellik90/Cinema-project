using API.Models;
namespace API.DTO;

public class SeatDTO
{
    public int SeatId { get; set; }
    public int SalonId { get; set; }
    public Salon Salon;

    public SeatDTO(int seatId, int salonId)
    {
        SeatId = seatId;
        SalonId = salonId;
    }

    public SeatDTO(){}
}