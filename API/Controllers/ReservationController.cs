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



    // [HttpPost]
    // public async Task<ActionResult<ReservationDTO>> CreateNewReservation(ReservationDTO reservationDTO)
    // {
    //     var reservation = new Reservation
    //     {
    //         ReservationId = reservationDTO.ReservationId,
    //         CustomerName = reservationDTO.CustomerName,
    //         PhoneNumber = reservationDTO.PhoneNumber,
    //         ShowId = reservationDTO.ShowId,
    //         NumberOfSeats = reservationDTO.NumberOfSeats,
    //         DateForReservation = reservationDTO.DateForReservation
    //     };
    //     await _reservationSeedData.CreateNewReservations(reservation);
    //     return Ok(reservationDTO);
    // }

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
                DateForReservation = reservationDTO.DateForReservation
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
            DateForReservation = r.DateForReservation

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
            DateForReservation = r.DateForReservation
        }).ToList();
    }

}