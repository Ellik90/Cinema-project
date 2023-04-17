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
        await UpdateSeats(reservations);
        return reservations;
    }

    private async Task UpdateSeats(Reservation reservation)
    {
        var reservedSeats = reservation.NumberOfSeats;

        var availableSeats = await _myDbContext.reservations
        .Where(r => r.MovieViewId == reservation.MovieViewId)
        .SumAsync(r => r.NumberOfSeats);

        availableSeats = availableSeats - reservedSeats;

        await _myDbContext.SaveChangesAsync();
    }

    public async Task<Reservation> DeleteReservation(Reservation reservation)
    {
        var existingReservation = await _myDbContext.reservations.FindAsync(reservation.ReservationId);
        if (existingReservation == null)
        {
            return null;
        }
        else
        {
            _myDbContext.reservations.Remove(existingReservation);
            await _myDbContext.SaveChangesAsync();
            return existingReservation;
        }
    }

}