using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Models;
using API.Data;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]

public class SalonController : ControllerBase
{
    SalonSeedData _salonSeedData;

    public SalonController(SalonSeedData salonSeedData)
    {
        _salonSeedData = salonSeedData;
    }


    [HttpGet]
    public async Task<ActionResult<List<Movie>>> GetSalons()
    {
        var salons = await _salonSeedData.GetSalons();
        return Ok(salons);
    }

    [HttpPost]
    public async Task<ActionResult<List<Movie>>> CreateMovie(Salon salon)
    {
        await _salonSeedData.CreateNewSalons(salon);
        return Ok(salon);
    }

    [HttpPut]
    public async Task<ActionResult<Movie>> UpdateSalon(Salon salon)
    {
        await _salonSeedData.UpdateSalon(salon);
        return Ok(salon);
    }

    [HttpDelete]
    public async Task<ActionResult<Movie>> DeleteSalon(Salon salon)
    {
        await _salonSeedData.DeleteSalon(salon);
        return Ok(salon);
    }


}