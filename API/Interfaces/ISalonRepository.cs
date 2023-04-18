using API.Models;

namespace API.Interface
{
    public interface ISalonRepository
    {
        Task<List<Salon>> GetSalons();
        Task<Salon> GetSalonById(int salonId);
        Task<Salon> CreateNewSalons(Salon salon);
        Task<Salon> UpdateSalon(Salon salon);
        Task<Salon> DeleteSalon(Salon salon);
    }
}