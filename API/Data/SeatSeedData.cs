using API.Models;
using Microsoft.EntityFrameworkCore;
namespace API.Data;

public class SeatSeedData
{
    MyDbContext _myDbContext;

    public SeatSeedData(MyDbContext myDbContext)
    {
        _myDbContext = myDbContext;
    }
}