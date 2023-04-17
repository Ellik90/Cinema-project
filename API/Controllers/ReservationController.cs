using Microsoft.AspNetCore.Mvc;
using API.Models;
using API.DTO;
using API.Data;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class ReservationController : ControllerBase
{
    ReservationRepository _reservationRepository;
    public ReservationController(ReservationRepository reservationRepository)
    {
        _reservationRepository = reservationRepository;
    }

    [HttpPost]
    public async Task<ActionResult<ReservationDTO>> CreateNewReservation(ReservationDTO reservationDTO)
    {
        try
        {
            var availableSeats = await _reservationRepository.GetAvailableSeatsForShow(reservationDTO.MovieViewId);

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

                var createdReservation = await _reservationRepository.CreateNewReservations(reservation);
                var createdReservationDTO = new ReservationDTO(createdReservation.ReservationId,
                createdReservation.CustomerName, createdReservation.PhoneNumber, createdReservation.MovieViewId,
                createdReservation.NumberOfSeats, createdReservation.ReservationPrice, createdReservation.DateForReservation);
                return Ok(createdReservationDTO);
            }
            else
            {
                return BadRequest("Inte tillräckligt med lediga platser");
            }
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ett problem uppstod.");
        }
    }

    [HttpGet]
    public async Task<ActionResult<List<ReservationDTO>>> GetAllMovieReservations()
    {
        try
        {
            var reservations = await _reservationRepository.GetReservations();
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
        catch (Exception ex)
        {
            return StatusCode(500, $"Ett fel uppstod: {ex.Message}");
        }
    }

    [HttpGet("{movieViewId}")]
    public async Task<ActionResult<List<ReservationDTO>>> GetReservationsForShow(int movieViewId)
    {
        try
        {
            var reservations = await _reservationRepository.GetReservationsForShow(movieViewId);

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
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ett fel uppstod vid hämtning av reservationer för en film.");
        }
    }

    [HttpDelete]
    public async Task<ActionResult<ReservationDTO>> DeletedReservationById(ReservationDTO reservationDTO)
    {
        try
        {
            var reservation = new Reservation()
            {
                ReservationId = reservationDTO.ReservationId,
            };

            var deletedReservation = await _reservationRepository.DeleteReservation(reservation);

            if (deletedReservation == null)
            {
                return NotFound();
            }

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
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ett fel uppstod vid borttagning av reservation.");
        }
    }
}