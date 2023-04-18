using API.Models;

namespace API.DTO
{
    public class SalonDTO
    {
        public int SalonId { get; set; }
        public string SalonName { get; set; }
        public int NumberOfSeats { get; set; }
        public int NumberOfRows { get; set; }
        public decimal SalonPrice { get; set; }
        public ICollection<Seat> Seats;

        public SalonDTO()
        {         
          
        }
    }
}
