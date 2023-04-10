using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Models;
using API.DTO;
using API.Data;
using System.Linq;


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

    // DENNA ÄR MIN KORREKTA METOD
    // [HttpPost]
    // public async Task<ActionResult<MovieViewDTO>> CreateNewMovieViews(MovieViewDTO movieViewDTO)
    // {
    //     // Hämta alla befintliga visningar som sker i samma salong på samma tidpunkt
    //     var existingMovieViews = await _movieViewSeedData.GetMovieViewsByDateAndSalonId(movieViewDTO.Date, movieViewDTO.SalonId)



    //     foreach (var existingMovieView in existingMovieViews)
    //     {
    //         if (existingMovieView.Date == movieViewDTO.Date)
    //         {
    //             return BadRequest($"En annan film, {existingMovieView.MovieTitle}, visas i samma salong vid samma tidpunkt.");
    //         }
    //     }

    //     // Skapa den nya visningen om ingen kollision hittades
    //     var movieView = new MovieView()
    //     {
    //         Date = movieViewDTO.Date,
    //         MovieId = movieViewDTO.MovieId,
    //         SalonId = movieViewDTO.SalonId,
    //         MovieTitle = movieViewDTO.MovieTitle
    //     };
    //     var addedMovieView = await _movieViewSeedData.CreateNewMovieView(movieView);
    //      var addedMovieViewDTO = new MovieViewDTO()
    //     {
    //         Date = addedMovieView.Date,
    //         MovieId = addedMovieView.MovieId,
    //         SalonId = addedMovieView.SalonId,
    //         MovieTitle = addedMovieView.MovieTitle
    //     };

    //     return Ok(addedMovieViewDTO);
    // }


    [HttpPost]
    public async Task<ActionResult<MovieViewDTO>> CreateNewMovieViews(MovieViewDTO movieViewDTO)
    {
        var existingMovieViews = (await _movieViewSeedData.GetMovieViewsByDateAndSalonId(movieViewDTO.Date, movieViewDTO.SalonId))
            .Where(existing => existing.Date == movieViewDTO.Date);


        if (existingMovieViews.Any())
        {
            var existingMovieTitles = string.Join(", ", existingMovieViews.Select(existing => existing.MovieTitle));
            return BadRequest($"En annan film, {existingMovieTitles}, visas i samma salong vid samma tidpunkt.");
        }

        var movieView = new MovieView()
        {
            Date = movieViewDTO.Date,
            MovieId = movieViewDTO.MovieId,
            SalonId = movieViewDTO.SalonId,
            MovieTitle = movieViewDTO.MovieTitle
        };

        var addedMovieView = await _movieViewSeedData.CreateNewMovieView(movieView);
        var addedMovieViewDTO = new MovieViewDTO()
        {
            Date = addedMovieView.Date,
            MovieId = addedMovieView.MovieId,
            SalonId = addedMovieView.SalonId,
            MovieTitle = addedMovieView.MovieTitle
        };

        return Ok(addedMovieViewDTO);
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
        movieView.MovieTitle = movieViewDTO?.MovieTitle; // Tilldelar MovieTitle från MovieViewDTO till movieView
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