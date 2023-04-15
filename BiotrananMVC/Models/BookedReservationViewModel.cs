namespace BiotrananMVC.Models;

public class BookedReservationViewModel
{
    public int ReservationId { get; set; }
    public string CustomerName { get; set; }
    public string PhoneNumber { get; set; }
    public int NumberOfSeats { get; set; }
    public DateTime DateForMovieView { get; set; }
    public string MovieTitle { get; set; }
    public BookedReservationViewModel(int reservationId, string customerName, string phoneNumber,
    int numberOfSeats, DateTime dateForMovieView, string movieTitle)
    {
        ReservationId = reservationId;
        CustomerName = customerName;
        PhoneNumber = phoneNumber;
        NumberOfSeats = numberOfSeats;
        DateForMovieView = dateForMovieView;
        MovieTitle = movieTitle;
    }

}