namespace API.Models;
public class Reservation
{
    public int ReservationId { get; set; }
    public string CustomerName { get; set; }
    public string PhoneNumber { get; set; }
    public int MovieViewId { get; set; }
    public int NumberOfSeats { get; set; }
    public decimal ReservationPrice { get; set; }
    public DateTime DateForReservation { get; set; }
    public MovieView MovieView{get;set;}

    public Reservation( string customerName, string phoneNumber, int movieViewId, int numberOfSeats, decimal reservationPrice, DateTime dateForReservation)
    {
        CustomerName = customerName;
        PhoneNumber = phoneNumber;
        MovieViewId = movieViewId;
        NumberOfSeats = numberOfSeats;
        ReservationPrice = reservationPrice;
        DateForReservation = dateForReservation;
    }
    public Reservation(){}
}

