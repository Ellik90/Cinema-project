using API.Models;
namespace API.DTO;

public class Reservation
{
    public int Id { get; set; }
    public float Price{ get; set; }
    public int RandomCode { get; set; }
    public int CustomerId { get; set; }
    public int MovieViewId { get; set; }

    public Reservation()
    {
        
    }
}