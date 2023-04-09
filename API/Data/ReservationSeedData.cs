using API.Models;
using Microsoft.EntityFrameworkCore;
namespace API.Data;

public class ReservationSeedData
{
    MyDbContext _myDbContext;

    public ReservationSeedData(MyDbContext myDbContext)
    {
        _myDbContext = myDbContext;
    }

    public async Task<List<Reservation>> GetReservations()
    {
        List<Reservation> reservations = new();
        try
        {
            reservations = await _myDbContext.reservations.ToListAsync();
            await _myDbContext.SaveChangesAsync();
            if (reservations.Count < 1)
            {
                throw new Exception();
            }
            return reservations;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public async Task<IEnumerable<Reservation>> GetReservationsForShow(int showId)
    {
        return await _myDbContext.reservations
            .Where(r => r.ShowId == showId)
            .ToListAsync();
    }

    public async Task<Reservation> CreateNewReservations(Reservation reservations)
    {
        _myDbContext.reservations.Add(reservations);
        await _myDbContext.SaveChangesAsync();
        return reservations;
    }
}