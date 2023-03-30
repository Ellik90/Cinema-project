using API.Models;
using Microsoft.EntityFrameworkCore;
namespace API.Data;

public class SalonSeedData
{
    MyDbContext _myDbContext;

    public SalonSeedData(MyDbContext myDbContext)
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

    // Lägg till en GetById

    public async Task<Salon> CreateNewSalons(Salon salon)
    {
        _myDbContext.salons.Add(salon);
        await _myDbContext.SaveChangesAsync();
        return salon;
    }

    public async Task<Salon> UpdateSalon(Salon salon)
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

    public async Task<Salon> DeleteSalon(Salon salon)
    {
        var existingSalon = await _myDbContext.salons.FindAsync(salon.SalonId);
        if (existingSalon == null)
        {
            return null;
        }
        else
        {
            _myDbContext.salons.Remove(existingSalon);
            await _myDbContext.SaveChangesAsync();
            return existingSalon;;
        }
    }
}