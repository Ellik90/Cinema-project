using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Models;
using API.DTO;
using API.Data;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class MovieViewController : ControllerBase
{
    MovieViewSeedData _movieViewSeedData;
    SeedData _seedData;
    public MovieViewController(MovieViewSeedData movieViewSeedData, SeedData seedData)
    {
        _movieViewSeedData = movieViewSeedData;
        _seedData = seedData;
    }


    [HttpPost]
    public async Task<ActionResult<MovieViewDTO>> CreateNewMovieView(MovieViewDTO movieViewDTO)
    {
        var movieView = new MovieView()
        {
            Date = movieViewDTO.Date,
            MovieId = movieViewDTO.MovieId,
            SalonId = movieViewDTO.SalonId
        };
        await _movieViewSeedData.CreateNewMovieView(movieView);
        return Ok(movieViewDTO);
    }

    [HttpGet]
    public async Task<ActionResult<List<MovieViewDTO>>> GetAllMovieViews()
    {
        var views = await _movieViewSeedData.GetMovieViews();
        if (views == null)
        {
            return Ok(new List<MovieViewDTO>());
        }
        var movieViewDTO = views.Select(v => new MovieViewDTO
        {
            MovieViewId = v.MovieViewId,
            MovieTitle = v.Movie?.Title,
            MovieId = v.MovieId,
            SalonId = v.SalonId,
            SalonName = v.Salon?.SalonName,
            Date = v.Date
        }).ToList();
        return Ok(movieViewDTO);
    }

    [HttpGet("getUpcomingViews")]
    public async Task<ActionResult<List<MovieViewDTO>>> GetUpcomingMovieViews()
    {
        var movies = await _seedData.GetMovies();
        var movieViews = await _movieViewSeedData.GetMovieViews();
        var upcomingMovieViews = movieViews.Where(v => v.Date >= DateTime.Now).ToList();
        var movieViewDTOs = upcomingMovieViews.Select(v => new MovieViewDTO
        {
            MovieViewId = v.MovieViewId,
            MovieTitle = movies.FirstOrDefault(m => m.MovieId == v.MovieId)?.Title,
            MovieId = v.MovieId,
            SalonId = v.SalonId,
            SalonName = v.Salon?.SalonName,
            Date = v.Date
        }).ToList();

        return Ok(movieViewDTOs);
    }





    [HttpPut]
    public async Task<ActionResult> PutMovieViews(MovieViewDTO movieViewDTO)
    {
        var movieViews = await _movieViewSeedData.GetMovieViews();

        var movieView = movieViews.Find(m => m.MovieViewId == movieViewDTO.MovieViewId);
        movieView.MovieViewId = movieViewDTO.MovieViewId;
        movieView.Date = movieViewDTO.Date;
        movieView.MovieId = movieViewDTO.MovieId;
        movieView.SalonId = movieViewDTO.SalonId;

        await _movieViewSeedData.UpdateMovieView(movieView);
        return Ok();
    }

    [HttpDelete]
    public async Task<ActionResult<MovieViewDTO>> DeleteMovieViewById(MovieViewDTO movieViewDTO)
    {
        var movieView = new MovieView()
        {
            MovieViewId = movieViewDTO.MovieViewId,
            Date = movieViewDTO.Date,
            MovieId = movieViewDTO.MovieId,
            SalonId = movieViewDTO.SalonId
        };
        var deletedMovie = await _movieViewSeedData.DeleteMovieById(movieView);

        var deletedMovieviewDTO = new MovieViewDTO()
        {
            MovieViewId = deletedMovie.MovieViewId,
            Date = deletedMovie.Date,
            MovieId = deletedMovie.MovieId,
            SalonId = deletedMovie.SalonId
        };
        return Ok(deletedMovieviewDTO);
    }

}