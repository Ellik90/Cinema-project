using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Models;
using API.DTO;
using API.Data;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class ReservationController : ControllerBase
{
    ReservationSeedData _reservationSeedData;
    public ReservationController(ReservationSeedData reservationSeedData)
    {
        _reservationSeedData = reservationSeedData;
    }


    [HttpPost]
    public async Task<ActionResult<ReservationDTO>> CreateNewReservation(ReservationDTO reservationDTO)
    {
        var availableSeats = await _reservationSeedData.GetAvailableSeatsForShow(reservationDTO.MovieViewId);

        if (availableSeats >= reservationDTO.NumberOfSeats)
        {
            var reservation = new Reservation
            {
                CustomerName = reservationDTO.CustomerName,
                PhoneNumber = reservationDTO.PhoneNumber,
                MovieViewId = reservationDTO.MovieViewId,
                NumberOfSeats = reservationDTO.NumberOfSeats,
                DateForReservation = reservationDTO.DateForReservation,
                ReservationPrice = reservationDTO.ReservationPrice
            };

            await _reservationSeedData.CreateNewReservations(reservation);

            return Ok(reservationDTO);
        }
        else
        {
            return BadRequest("Not enough available seats");
        }
    }

    [HttpGet]
    public async Task<ActionResult<List<ReservationDTO>>> GetAllMovieViews()
    {
        var reservations = await _reservationSeedData.GetReservations();
        if (reservations == null)
        {
            return Ok(new List<ReservationDTO>());
        }
        var reservationDTO = reservations.Select(r => new ReservationDTO
        {
            ReservationId = r.ReservationId,
            CustomerName = r.CustomerName,
            PhoneNumber = r.PhoneNumber,
            MovieViewId = r.MovieViewId,
            NumberOfSeats = r.NumberOfSeats,
            DateForReservation = r.DateForReservation,
            ReservationPrice = r.ReservationPrice

        }).ToList();
        return Ok(reservationDTO);
    }

    [HttpGet("{movieViewId}")]
    public async Task<ActionResult<List<ReservationDTO>>> GetReservationsForShow(int movieViewId)
    {
        var reservations = await _reservationSeedData.GetReservationsForShow(movieViewId);

        if (reservations == null || reservations.Count() == 0)
        {
            return NotFound();
        }

        return reservations.Select(r => new ReservationDTO
        {
            ReservationId = r.ReservationId,
            CustomerName = r.CustomerName,
            PhoneNumber = r.PhoneNumber,
            MovieViewId = r.MovieViewId,
            NumberOfSeats = r.NumberOfSeats,
            DateForReservation = r.DateForReservation,
            ReservationPrice = r.ReservationPrice
        }).ToList();
    }

    [HttpDelete]
    public async Task<ActionResult<ReservationDTO>> DeletedReservationById(ReservationDTO reservationDTO)
    {
        var reservation = new Reservation()
        {
            ReservationId = reservationDTO.ReservationId,
        };
        var deletedReservation = await _reservationSeedData.DeleteReservation(reservation);

        var deletedReservationDTO = new ReservationDTO()
        {
            ReservationId = deletedReservation.ReservationId,
            CustomerName = deletedReservation.CustomerName,
            PhoneNumber = deletedReservation.PhoneNumber,
            MovieViewId = deletedReservation.MovieViewId,
            NumberOfSeats = deletedReservation.NumberOfSeats,
            ReservationPrice = deletedReservation.ReservationPrice,
            DateForReservation = deletedReservation.DateForReservation
        };
        return Ok(deletedReservationDTO);
    }
}