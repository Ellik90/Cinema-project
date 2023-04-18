using API.Models;

namespace API.Interface
{
    public interface ISeatRepository
    {
        Task<Seat> CreateNewSeat(Seat seat);
        Task<List<Seat>> GetSeats();
        Task<List<Seat>> GetSeatsById(List<int> seatIds);
        Task<Seat> UpdateSeat(Seat seat);
        Task<bool> DeleteSeats(List<int> seatIds);
    }
}
