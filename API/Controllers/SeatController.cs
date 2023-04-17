using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Models;
using API.DTO;
using API.Data;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class SeatController : ControllerBase
{
    ISeatRepository _iSeatRepository;

    public SeatController(ISeatRepository iSeatRepository)
    {
        _iSeatRepository = iSeatRepository;
    }

    [HttpPost]
    public async Task<ActionResult<SeatDTO>> CreateSeat(SeatDTO seatDTO)
    {
        try
        {
            var seat = new Seat
            {
                SeatId = seatDTO.SeatId,
                SalonId = seatDTO.SalonId,
            };
            await _iSeatRepository.CreateNewSeat(seat);
            return Ok(seatDTO);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Det gick inte att skapa en sits: {ex.Message}");
        }
    }

    [HttpGet]
    public async Task<ActionResult<SeatDTO>> GetSeats()
    {
        try
        {
            var seats = await _iSeatRepository.GetSeats();
            if (seats == null)
            {
                return Ok(new List<SeatDTO>());
            }
            var seatDTO = seats.Select(s => new SeatDTO
            {
                SalonId = s.SalonId,
                SeatId = s.SeatId,
            }).ToList();
            return Ok(seatDTO);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Ett fel uppstod vid hämtning av säten: {ex.Message}");
        }
    }

    [HttpGet("seatsById/{seatIds}")]
    public async Task<ActionResult<SeatDTO>> GetSeatById(string seatIds)
    {
        try
        {
            List<int> seatIdsList = seatIds.Split(",").Select(int.Parse).ToList();
            List<Seat> seats = await _iSeatRepository.GetSeatsById(seatIdsList);
            var seatDto = seats.Select(s => new SeatDTO
            {
                SalonId = s.SalonId,
                SeatId = s.SeatId,
            }).ToList();
            return Ok(seatDto);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Ett fel uppstod: {ex.Message}");
        }
    }

    [HttpPut]
    public async Task<ActionResult<SeatDTO>> UpdateSeats(SeatDTO seatDTO)
    {
        try
        {
            var seats = await _iSeatRepository.GetSeats();

            var seat = seats.Find(s => s.SeatId == seatDTO.SeatId);
            if (seat == null)
            {
                return NotFound("säte hittades inte");
            }
            seat.SeatId = seatDTO.SeatId;
            seat.SalonId = seatDTO.SalonId;

            await _iSeatRepository.UpdateSeat(seat);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ett fel uppstod: " + ex.Message);
        }
    }

    [HttpDelete]
    public async Task<ActionResult> DeleteSeat(string seatDTO)
    {
        try
        {
            List<int> seatIdsList = seatDTO.Split(",").Select(int.Parse).ToList();
            bool success = await _iSeatRepository.DeleteSeats(seatIdsList);
            if (success)
            {
                return Ok("Säten borttagna");
            }
            else
            {
                return NotFound("Kunde inte hitta de angivna sätena");
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Ett fel inträffade: {ex.Message}");
        }
    }
}

