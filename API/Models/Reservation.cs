namespace API.Models;
public class Reservation
{
    public int ReservationId { get; set; }
    public string CustomerName { get; set; }
    public string PhoneNumber { get; set; }
    public int ShowId { get; set; }
    public int NumberOfSeats { get; set; }
    public DateTime DateForReservation { get; set; }

    public Reservation( string customerName, string phoneNumber, int showId, int numberOfSeats, DateTime dateForReservation)
    {
        CustomerName = customerName;
        PhoneNumber = phoneNumber;
        ShowId = showId;
        NumberOfSeats = numberOfSeats;
        DateForReservation = dateForReservation; // ska denna innehålla reservationsId?
    }
    public Reservation(){}
}

