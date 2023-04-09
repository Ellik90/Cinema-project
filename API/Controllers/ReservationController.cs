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
        var reservation = new Reservation
        {
             ReservationId = reservationDTO.ReservationId,
            CustomerName = reservationDTO.CustomerName,
            PhoneNumber = reservationDTO.PhoneNumber,
            ShowId = reservationDTO.ShowId,
            NumberOfSeats = reservationDTO.NumberOfSeats,
            DateForReservation = reservationDTO.DateForReservation
        };
        await _reservationSeedData.CreateNewReservations(reservation);
        return Ok(reservationDTO);
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
            ShowId = r.ShowId,
            NumberOfSeats = r.NumberOfSeats,
            DateForReservation = r.DateForReservation
            
        }).ToList();
        return Ok(reservationDTO);
    }

    // [HttpGet("getUpcomingViews")]
    // public async Task<ActionResult<List<MovieViewDTO>>> GetUpcomingMovieViews()
    // {
    //     var movies = await _seedData.GetMovies();
    //     var movieViews = await _movieViewSeedData.GetMovieViews();
    //     var upcomingMovieViews = movieViews.Where(v => v.Date >= DateTime.Now).ToList();
    //     var movieViewDTOs = upcomingMovieViews.Select(v => new MovieViewDTO
    //     {
    //         MovieViewId = v.MovieViewId,
    //         MovieTitle = movies.FirstOrDefault(m => m.MovieId == v.MovieId)?.Title,
    //         MovieId = v.MovieId,
    //         SalonId = v.SalonId,
    //         SalonName = v.Salon?.SalonName,
    //         Date = v.Date
    //     }).ToList();

    //     return Ok(movieViewDTOs);
    // }





    // [HttpPut]
    // public async Task<ActionResult> PutMovieViews(MovieViewDTO movieViewDTO)
    // {
    //     var movieViews = await _movieViewSeedData.GetMovieViews();

    //     var movieView = movieViews.Find(m => m.MovieViewId == movieViewDTO.MovieViewId);
    //     movieView.MovieViewId = movieViewDTO.MovieViewId;
    //     movieView.Date = movieViewDTO.Date;
    //     movieView.MovieId = movieViewDTO.MovieId;
    //     movieView.SalonId = movieViewDTO.SalonId;

    //     await _movieViewSeedData.UpdateMovieView(movieView);
    //     return Ok();
    // }

    // [HttpDelete]
    // public async Task<ActionResult<MovieViewDTO>> DeleteMovieViewById(MovieViewDTO movieViewDTO)
    // {
    //     var movieView = new MovieView()
    //     {
    //         MovieViewId = movieViewDTO.MovieViewId,
    //         Date = movieViewDTO.Date,
    //         MovieId = movieViewDTO.MovieId,
    //         SalonId = movieViewDTO.SalonId
    //     };
    //     var deletedMovie = await _movieViewSeedData.DeleteMovieById(movieView);

    //     var deletedMovieviewDTO = new MovieViewDTO()
    //     {
    //         MovieViewId = deletedMovie.MovieViewId,
    //         Date = deletedMovie.Date,
    //         MovieId = deletedMovie.MovieId,
    //         SalonId = deletedMovie.SalonId
    //     };
    //     return Ok(deletedMovieviewDTO);
    // }

}