using API.Models;
namespace API.DTO;

public class ReservationDTO
{
      public int ReservationId { get; set; }
    public string CustomerName { get; set; }
    public string PhoneNumber { get; set; }
    public int MovieViewId { get; set; }
    public int NumberOfSeats { get; set; }
    public DateTime DateForReservation { get; set; }

    public ReservationDTO(int reservationId, string customerName, string phoneNumber, int movieViewId, int numberOfSeats, DateTime dateForReservation)
    {
        ReservationId = reservationId;
        CustomerName = customerName;
        PhoneNumber = phoneNumber;
        MovieViewId = movieViewId;
        NumberOfSeats = numberOfSeats;
        DateForReservation = dateForReservation;
    }
    public ReservationDTO(){}
}