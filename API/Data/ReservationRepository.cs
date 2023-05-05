using API.Models;
using Microsoft.EntityFrameworkCore;
using API.Interface;

namespace API.Data;
public class ReservationRepository : IReservationRepository
{
    MyDbContext _myDbContext;

    public ReservationRepository(MyDbContext myDbContext)
    {
        _myDbContext = myDbContext;
    }

    public async Task<int> GetAvailableSeatsForShow(int movieViewId)
    {
        try
        {
            var show = await _myDbContext.movieViews
                .Include(mv => mv.Salon)
                .FirstOrDefaultAsync(s => s.MovieViewId == movieViewId);

            if (show == null)
            {
                throw new ArgumentException("fel moviview id");
            }

            var reservedSeats = await _myDbContext.reservations
                .Where(r => r.MovieViewId == movieViewId)
                .SumAsync(r => r.NumberOfSeats);
                
            return show.Salon.NumberOfSeats - reservedSeats;
        }
        catch (Exception ex)
        {
            throw new Exception("Ett fel uppstod", ex);
        }
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
        try
        {
            return await _myDbContext.reservations
                .Where(r => r.MovieViewId == movieViewId)
                .ToListAsync();
        }
        catch (Exception ex)
        {
            return Enumerable.Empty<Reservation>();
        }
    }

    public async Task<Reservation> CreateNewReservations(Reservation reservations)
    {
        try
        {
            _myDbContext.reservations.Add(reservations);
            await _myDbContext.SaveChangesAsync();
            await UpdateSeats(reservations);
            var createdReservation = await _myDbContext.reservations.OrderByDescending(r => r.ReservationId).FirstOrDefaultAsync();
            return createdReservation;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    private async Task UpdateSeats(Reservation reservation)
    {
        try
        {
            var reservedSeats = reservation.NumberOfSeats;

            var availableSeats = await _myDbContext.reservations
            .Where(r => r.MovieViewId == reservation.MovieViewId)
            .SumAsync(r => r.NumberOfSeats);

            availableSeats = availableSeats - reservedSeats;
            await _myDbContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception("Ett fel uppstod", ex);
        }
    }

    public async Task<Reservation> DeleteReservation(Reservation reservation)
    {
        try
        {
            var existingReservation = await _myDbContext.reservations.FindAsync(reservation.ReservationId);
            if (existingReservation == null)
            {
                throw new ArgumentException("Reservation not found");
            }
            _myDbContext.reservations.Remove(existingReservation);
            await _myDbContext.SaveChangesAsync();
            return existingReservation;
        }
        catch (Exception ex)
        {
            return null;
        }
    }
}