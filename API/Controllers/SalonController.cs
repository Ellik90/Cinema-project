using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Models;
using API.DTO;
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
    public async Task<ActionResult<List<SalonDTO>>> GetSalons()
    {
        var salons = await _salonSeedData.GetSalons();
        if (salons == null)
        {
            return Ok(new List<SalonDTO>());
        }
        var salonDTOs = salons.Select(s => new SalonDTO
        {
            SalonId = s.SalonId,
            SalonName = s.SalonName,
            NumberOfRows = s.NumberOfRows,
            NumberOfSeats = s.NumberOfSeats
        }).ToList();
        return Ok(salonDTOs);
    }

    [HttpGet("salonId")]
    public async Task<ActionResult<SalonDTO>> GetSalonById(int salonId) 
    {
        var salonDTO = await _salonSeedData.GetSalonById(salonId);
        var salon = new SalonDTO
        {
            SalonId = salonDTO.SalonId,         
            SalonName = salonDTO.SalonName,
            NumberOfRows = salonDTO.NumberOfRows,
            NumberOfSeats = salonDTO.NumberOfSeats
        };
        return Ok(salonDTO);
    }

    [HttpPost]
    public async Task<ActionResult<SalonDTO>> CreateSalon(SalonDTO salonDTO)
    {
        var salon = new Salon
        {
            SalonId = salonDTO.SalonId,
            SalonName = salonDTO.SalonName,
            NumberOfRows = salonDTO.NumberOfRows,
            NumberOfSeats = salonDTO.NumberOfSeats
        };
        await _salonSeedData.CreateNewSalons(salon);
        return Ok(salonDTO);
    }

    [HttpPut]
    public async Task<ActionResult<SalonDTO>> UpdateSalon(SalonDTO salonDTO)
    {
        var salon = await _salonSeedData.GetSalonById(salonDTO.SalonId);

        salon.SalonId = salonDTO.SalonId;
        salon.SalonName = salonDTO.SalonName;
        salon.NumberOfRows = salonDTO.NumberOfRows;
        salon.NumberOfSeats = salonDTO.NumberOfSeats;

        await _salonSeedData.UpdateSalon(salon);
        return Ok(salonDTO);
    }

    [HttpDelete]
    public async Task<ActionResult<SalonDTO>> DeleteSalon(SalonDTO salonDTO)
    {
        var salon = await _salonSeedData.GetSalonById(salonDTO.SalonId);
        await _salonSeedData.DeleteSalon(salon);
        return Ok(salonDTO);
    }

}