using API.Models;
using Microsoft.EntityFrameworkCore;
using API.Interface;

namespace API.Data;
public class SalonRepository : ISalonRepository
{
    MyDbContext _myDbContext;

    public SalonRepository(MyDbContext myDbContext)
    {
        _myDbContext = myDbContext;
    }

    public async Task<List<Salon>> GetSalons()
    {
        List<Salon> salons = new();
        try
        {
            salons = await _myDbContext.salons.Include(s => s.Seats).ToListAsync();
            await _myDbContext.SaveChangesAsync();
            if (salons.Count < 1)
            {
                throw new Exception();
            }
            return salons;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public async Task<Salon> GetSalonById(int seatId)
    {
        try
        {
            List<Salon> getSalon = new();
            getSalon = await _myDbContext.salons.ToListAsync();
            return getSalon.Find(g => g.SalonId == seatId);
        }
        catch (Exception)
        {
            return null;
        }
    }

    public async Task<Salon> CreateNewSalons(Salon salon)
    {
        try
        {
            _myDbContext.salons.Add(salon);
            await _myDbContext.SaveChangesAsync();
            return salon;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public async Task<Salon> UpdateSalon(Salon salon)
    {
        try
        {
            var existingSalon = await _myDbContext.salons.Include(s => s.Seats).FirstOrDefaultAsync(s => s.SalonId == salon.SalonId);
            if (existingSalon == null)
            {
                return null;
            }
            else
            {
                existingSalon.Seats = salon.Seats;
                existingSalon.SalonName = salon.SalonName;
                existingSalon.NumberOfRows = salon.NumberOfRows;
                existingSalon.NumberOfSeats = salon.NumberOfSeats;
                await _myDbContext.SaveChangesAsync();
                return existingSalon;
            }
        }
        catch (Exception ex)
        {
            throw new Exception("Ett fel uppstod", ex);
        }
    }

    public async Task<Salon> DeleteSalon(Salon salon)
    {
        try
        {
            var existingSalon = await _myDbContext.salons.FindAsync(salon.SalonId);
            if (existingSalon == null)
            {
                throw new ArgumentException("Salon not found");
            }
            _myDbContext.salons.Remove(existingSalon);
            await _myDbContext.SaveChangesAsync();
            return existingSalon;
        }
        catch (Exception ex)
        {
            throw new Exception("Ett fel uppstod", ex);
        }
    }
}