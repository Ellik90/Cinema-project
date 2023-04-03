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

    public MovieViewController(MovieViewSeedData movieViewSeedData)
    {
        _movieViewSeedData = movieViewSeedData;
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
    public async Task<ActionResult<MovieViewDTO>> GetMovieViews()
    {
        var movieViews = await _movieViewSeedData.GetMovieViews();
        // ska bara hämta alla kommande visningar
        return Ok(movieViews.Select(v => new MovieViewDTO(v.MovieViewId, v.MovieId, v.SalonId, v.Date)));
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