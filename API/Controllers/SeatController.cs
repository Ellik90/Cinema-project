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
    SeatSeedData _seatSeedData;

    public SeatController(SeatSeedData seatSeedData)
    {
        _seatSeedData = seatSeedData;
    }

    [HttpPost]
    public async Task<ActionResult<SeatDTO>> CreateSeat(SeatDTO seatDTO)
    {
        var seat = new Seat
        {
            SeatId = seatDTO.SeatId,
            SalonId = seatDTO.SalonId,
        };
        await _seatSeedData.CreateNewSeat(seat);
        return Ok(seatDTO);
    }

    [HttpGet]
    public async Task<ActionResult<SeatDTO>> GetSeats()
    {
        var seats = await _seatSeedData.GetSeats();
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

    [HttpGet("seatsById/{seatIds}")]
    public async Task<ActionResult<SeatDTO>> GetSeatById(string seatIds)
    {
        List<int> seatIdsList = seatIds.Split(",").Select(int.Parse).ToList();
        List<Seat> seats = await _seatSeedData.GetSeatsById(seatIdsList);
        var seatDto = seats.Select(s => new SeatDTO
        {
            SalonId = s.SalonId,
            SeatId = s.SeatId,
        }).ToList();
        return Ok(seatDto);
    }

    [HttpPut]
    public async Task<ActionResult<SeatDTO>> UpdateSeats(SeatDTO seatDTO)
    {
        var seats = await _seatSeedData.GetSeats();

        var seat = seats.Find(s => s.SeatId == seatDTO.SeatId);
        seat.SeatId = seatDTO.SeatId;
        seat.SalonId = seatDTO.SalonId;

        await _seatSeedData.UpdateSeat(seat);
        return Ok();
    }

    [HttpDelete]
    public async Task<ActionResult> DeleteSeat(string seatDTO)
    {
        List<int> seatIdsList = seatDTO.Split(",").Select(int.Parse).ToList();
        bool success = await _seatSeedData.DeleteSeats(seatIdsList);
        if (success)
        {
            return Ok("Säten borttagna");
        }
        else
        {
            return NotFound("Kunde inte hitta de angivna sätena");
        }
    }

}

