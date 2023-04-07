using System.Collections.Generic;
using API.Models;

namespace API.DTO
{
    public class SalonDTO
    {
        public int SalonId { get; set; }
        public string SalonName { get; set; }
        public int NumberOfSeats { get; set; }
        public int NumberOfRows { get; set; }
        public ICollection<Seat> Seats;

        public SalonDTO()
        {
        }
    }
}



// using API.Models;
// namespace API.DTO;

// public class SalonDTO
// {
//     public int SalonId { get; set; }
//     public string SalonName { get; set; }
//     public int NumberOfSeats { get; set; }
//     public int NumberOfRows { get; set; }
//     public ICollection<Seat> Seats { get; set; }

//     public SalonDTO(string salonName, int numberOfSeats, int numberOfRows, ICollection<Seat> seats)
//     {
//         SalonName = salonName;
//         SalonName = salonName;
//         NumberOfSeats = numberOfSeats;
//         NumberOfRows = numberOfRows;
//         Seats = seats;
//     }
//     public SalonDTO(){}
// }