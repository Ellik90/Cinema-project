using API.Models;
using Microsoft.EntityFrameworkCore;
namespace API.Data;

public class SeatRepository : ISeatRepository
{
    MyDbContext _myDbContext;

    public SeatRepository(MyDbContext myDbContext)
    {
        _myDbContext = myDbContext;
    }

    public async Task<Seat> CreateNewSeat(Seat seat)
    {
        try
        {
            await _myDbContext.seats.AddAsync(seat);
            await _myDbContext.SaveChangesAsync();
            return seat;
        }
        catch (Exception ex)
        {
            throw new Exception("Ett fel uppstod: " + ex.Message);
        }
    }

    public async Task<List<Seat>> GetSeats()
    {
        try
        {
            return await _myDbContext.seats.ToListAsync();
        }
        catch (Exception ex)
        {
            throw new Exception("Ett fel uppstod.", ex);
        }
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
        try
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
        catch (Exception ex)
        {
            return null;
        }
    }

    public async Task<bool> DeleteSeats(List<int> seatIds)
    {
        try
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
        catch (Exception ex)
        {
            return false;
        }
    }
}