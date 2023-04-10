using API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.Sqlite;

namespace API.Data;

public class ReservationSeedData
{
    MyDbContext _myDbContext;

    public ReservationSeedData(MyDbContext myDbContext)
    {
        _myDbContext = myDbContext;
    }

    public async Task<int> GetAvailableSeatsForShow(int movieViewId)
    {
        var show = await _myDbContext.movieViews
        .Include(mv => mv.Salon)
        .FirstOrDefaultAsync(s => s.MovieViewId == movieViewId);

        if (show == null)
        {
            throw new ArgumentException("Invalid MovieViewId");
        }

        var reservedSeats = await _myDbContext.reservations
            .Where(r => r.MovieViewId == movieViewId)
            .SumAsync(r => r.NumberOfSeats);

        return show.Salon.NumberOfSeats - reservedSeats;
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

    public async Task<IEnumerable<Reservation>> GetReservationsForShow(int movieViewId)
    {
        return await _myDbContext.reservations
            .Where(r => r.MovieViewId == movieViewId)
            .ToListAsync();
    }

    public async Task<Reservation> CreateNewReservations(Reservation reservations)
    {
        _myDbContext.reservations.Add(reservations);
        await _myDbContext.SaveChangesAsync();
        return reservations;
    }
}