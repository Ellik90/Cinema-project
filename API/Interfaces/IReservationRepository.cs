using API.Models;

namespace API.Interface
{
    public interface IReservationRepository
    {
        Task<int> GetAvailableSeatsForShow(int movieViewId);
        Task<List<Reservation>> GetReservations();
        Task<IEnumerable<Reservation>> GetReservationsForShow(int movieViewId);
        Task<Reservation> CreateNewReservations(Reservation reservations);
        Task<Reservation> DeleteReservation(Reservation reservation);
    }

}