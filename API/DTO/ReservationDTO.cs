using API.Models;
namespace API.DTO;

public class ReservationDTO
{
      public int ReservationId { get; set; }
    public string CustomerName { get; set; }
    public string PhoneNumber { get; set; }
    public int ShowId { get; set; }
    public int NumberOfSeats { get; set; }
    public DateTime DateForReservation { get; set; }

    public ReservationDTO(int reservationId, string customerName, string phoneNumber, int showId, int numberOfSeats, DateTime dateForReservation)
    {
        ReservationId = reservationId;
        CustomerName = customerName;
        PhoneNumber = phoneNumber;
        ShowId = showId;
        NumberOfSeats = numberOfSeats;
        DateForReservation = dateForReservation;
    }
    public ReservationDTO(){}
}