using API.Models;
using Microsoft.EntityFrameworkCore;
namespace API.Data;

public class SeatSeedData
{
    MyDbContext _myDbContext;

    public SeatSeedData(MyDbContext myDbContext)
    {
        _myDbContext = myDbContext;
    }

    public async Task<Seat> CreateNewSeat(Seat seat)
    {
        await _myDbContext.seats.AddAsync(seat);
        await _myDbContext.SaveChangesAsync();
        return seat;
    }

    public async Task<List<Seat>> GetSeats()
    {
        List<Seat> seats = new();

        seats = await _myDbContext.seats.ToListAsync();
        return seats;
    }

    public async Task<List<Seat>> GetSeatsById(List<int> seatIds)
    {
        try
        {
            List<Seat> getSeats = await _myDbContext.seats.ToListAsync();
            return getSeats.Where(g => seatIds.Contains(g.SeatId)).ToList();
        }
        catch (Exception)
        {
            return null;
        }
    }

    public async Task<Seat> UpdateSeat(Seat seat)
    {
        var existingSeat = await _myDbContext.seats.FindAsync(seat.SeatId);
        if (existingSeat == null)
        {
            return null;
        }
        else
        {
            existingSeat.SalonId = seat.SalonId;
            await _myDbContext.SaveChangesAsync();
            return existingSeat;
        }
    }

    public async Task<bool> DeleteSeats(List<int> seatIds)
    {
        var seatsToDelete = await _myDbContext.seats.Where(s => seatIds.Contains(s.SeatId)).ToListAsync();
        if (seatsToDelete != null && seatsToDelete.Any())
        {
            _myDbContext.seats.RemoveRange(seatsToDelete);
            await _myDbContext.SaveChangesAsync();
            return true;
        }
        return false;
    }

}