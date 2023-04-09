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

    // public async Task<Salon> GetSalonById(int seatId)
    // {
    //     try
    //     {
    //         List<Salon> getSalon = new();
    //         getSalon = await _myDbContext.salons.ToListAsync();
    //         return getSalon.Find(g => g.SalonId == seatId);
    //     }
    //     catch (Exception)
    //     {
    //         return null;
    //     }
    // }

    public async Task<Reservation> CreateNewReservations(Reservation reservations)
    {
        _myDbContext.reservations.Add(reservations);
        await _myDbContext.SaveChangesAsync();
        return reservations;
    }

    // public async Task<Salon> UpdateSalon(Salon salon)
    // {
    //     var existingSalon = await _myDbContext.salons.Include(s => s.Seats).FirstOrDefaultAsync(s => s.SalonId == salon.SalonId);
    //     if (existingSalon == null)
    //     {
    //         return null;
    //     }
    //     else
    //     {
    //         existingSalon.Seats = salon.Seats;
    //         existingSalon.SalonName = salon.SalonName;
    //         existingSalon.NumberOfRows = salon.NumberOfRows;
    //         existingSalon.NumberOfSeats = salon.NumberOfSeats;
    //         await _myDbContext.SaveChangesAsync();
    //         return existingSalon;
    //     }
    // }

    // public async Task<Salon> DeleteSalon(Salon salon)
    // {
    //     var existingSalon = await _myDbContext.salons.FindAsync(salon.SalonId);
    //     if (existingSalon == null)
    //     {
    //         return null;
    //     }
    //     else
    //     {
    //         _myDbContext.salons.Remove(existingSalon);
    //         await _myDbContext.SaveChangesAsync();
    //         return existingSalon; ;
    //     }
    // }
}