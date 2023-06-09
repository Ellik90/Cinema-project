using Microsoft.AspNetCore.Mvc;
using API.Models;
using API.DTO;
using API.Interface;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class SalonController : ControllerBase
{
    ISalonRepository _iSalonRepository;

    public SalonController(ISalonRepository iSalonRepository)
    {
        _iSalonRepository = iSalonRepository;
    }

    [HttpGet]
    public async Task<ActionResult<List<SalonDTO>>> GetSalons()
    {
        try
        {
            var salons = await _iSalonRepository.GetSalons();
            if (salons == null)
            {
                return Ok(new List<SalonDTO>());
            }
            var salonDTOs = salons.Select(s => new SalonDTO
            {
                SalonId = s.SalonId,
                SalonName = s.SalonName,
                NumberOfRows = s.NumberOfRows,
                NumberOfSeats = s.NumberOfSeats,
                SalonPrice = s.SalonPrice
            }).ToList();
            return Ok(salonDTOs);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Det gick inte att hämta salonger.");
        }
    }

    [HttpGet("salonId")]
    public async Task<ActionResult<SalonDTO>> GetSalonById(int salonId)
    {
        try
        {
            var salonDTO = await _iSalonRepository.GetSalonById(salonId);
            if (salonDTO == null)
            {
                return NotFound();
            }
            var salon = new SalonDTO
            {
                SalonId = salonDTO.SalonId,
                SalonName = salonDTO.SalonName,
                NumberOfRows = salonDTO.NumberOfRows,
                NumberOfSeats = salonDTO.NumberOfSeats,
                SalonPrice = salonDTO.SalonPrice
            };
            return Ok(salon);
        }
        catch (Exception ex)
        {
            return StatusCode(500);
        }
    }

    [HttpPost]
    public async Task<ActionResult<SalonDTO>> CreateSalon(SalonDTO salonDTO)
    {
        try
        {
            var salon = new Salon
            {
                SalonId = salonDTO.SalonId,
                SalonName = salonDTO.SalonName,
                NumberOfRows = salonDTO.NumberOfRows,
                NumberOfSeats = salonDTO.NumberOfSeats,
                SalonPrice = salonDTO.SalonPrice
            };
            await _iSalonRepository.CreateNewSalons(salon);
            return Ok(salonDTO);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Ett fel uppstod: {ex.Message}");
        }
    }

    [HttpPut]
    public async Task<ActionResult<SalonDTO>> UpdateSalon(SalonDTO salonDTO)
    {
        try
        {
            var salon = await _iSalonRepository.GetSalonById(salonDTO.SalonId);

            if (salon == null)
            {
                return NotFound();
            }
            salon.SalonName = salonDTO.SalonName;
            salon.NumberOfRows = salonDTO.NumberOfRows;
            salon.NumberOfSeats = salonDTO.NumberOfSeats;
            salon.SalonPrice = salonDTO.SalonPrice;

            await _iSalonRepository.UpdateSalon(salon);
            return Ok(salonDTO);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Ett fel uppstod.");
        }
    }

    [HttpDelete]
    public async Task<ActionResult<SalonDTO>> DeleteSalon(SalonDTO salonDTO)
    {
        try
        {
            var salon = await _iSalonRepository.GetSalonById(salonDTO.SalonId);
            await _iSalonRepository.DeleteSalon(salon);
            return Ok(salonDTO);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Ett fel uppstod vid borttagning av salonger.");
        }
    }
}