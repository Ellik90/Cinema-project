using Microsoft.AspNetCore.Mvc;
using API.Models;
using API.DTO;
using API.Interface;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class MovieViewController : ControllerBase
{
    IMovieViewRepository _iMovieViewRepository;
    IMovieRepository _iMovieRepository;
    public MovieViewController(IMovieViewRepository iMovieViewRepository, IMovieRepository iMovieRepository)
    {
        _iMovieViewRepository = iMovieViewRepository;
        _iMovieRepository = iMovieRepository;
    }
    

    [HttpPost]
    public async Task<ActionResult<MovieViewDTO>> CreateNewMovieViews(MovieViewDTO movieViewDTO)
    {
        try
        {
            var existingMovieViews = (await _iMovieViewRepository.GetMovieViewsByDateAndSalonId(movieViewDTO.Date, movieViewDTO.SalonId))
                .Where(existing => existing.Date == movieViewDTO.Date);

            if (existingMovieViews.Any())
            {
                var existingMovieTitles = string.Join(", ", existingMovieViews.Select(existing => existing.MovieTitle));
                return BadRequest($"En annan film, {existingMovieTitles}, visas i samma salong vid samma tidpunkt.");
            }

            var movie = await _iMovieRepository.GetMovieById(movieViewDTO.MovieId);
            var movieViews = await _iMovieViewRepository.GetMovieViewsByMovieId(movieViewDTO.MovieId);

            if (movieViews.Count() >= movie.MaxViews)
            {
                return BadRequest($"Max antal visningar har redan uppnåtts för filmen {movie.Title}");
            }

            var movieView = new MovieView()
            {
                Date = movieViewDTO.Date,
                MovieId = movieViewDTO.MovieId,
                SalonId = movieViewDTO.SalonId,
                MovieTitle = movieViewDTO.MovieTitle
            };

            var addedMovieView = await _iMovieViewRepository.CreateNewMovieView(movieView);
            var addedMovieViewDTO = new MovieViewDTO()
            {
                Date = addedMovieView.Date,
                MovieId = addedMovieView.MovieId,
                SalonId = addedMovieView.SalonId,
                MovieTitle = addedMovieView.MovieTitle
            };

            return Ok(addedMovieViewDTO);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }


    [HttpGet]
    public async Task<ActionResult<List<MovieViewDTO>>> GetAllMovieViews()
    {
        try
        {
            var views = await _iMovieViewRepository.GetMovieViews();
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
                Date = v.Date,
                AvailableSeats = v.AvailableSeats
            }).ToList();
            return Ok(movieViewDTO);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpGet("getUpcomingViews")]
    public async Task<ActionResult<List<MovieViewDTO>>> GetUpcomingMovieViews()
    {
        try
        {
            var movies = await _iMovieRepository.GetMovies();
            var movieViews = await _iMovieViewRepository.GetMovieViews();
            var upcomingMovieViews = movieViews.Where(v => v.Date >= DateTime.Now).ToList();
            var movieViewDTOs = upcomingMovieViews.Select(v => new MovieViewDTO
            {
                MovieViewId = v.MovieViewId,
                MovieTitle = movies.FirstOrDefault(m => m.MovieId == v.MovieId)?.Title,
                MovieId = v.MovieId,
                SalonId = v.SalonId,
                SalonName = v.Salon?.SalonName,
                Date = v.Date,
                AvailableSeats = v.AvailableSeats,
            }).ToList();

            return Ok(movieViewDTOs);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpPut]
    public async Task<ActionResult> PutMovieViews(MovieViewDTO movieViewDTO)
    {
        try
        {
            var movieViews = await _iMovieViewRepository.GetMovieViews();

            var movieView = movieViews.Find(m => m.MovieViewId == movieViewDTO.MovieViewId);
            movieView.MovieViewId = movieViewDTO.MovieViewId;
            movieView.MovieTitle = movieViewDTO?.MovieTitle;
            movieView.Date = movieViewDTO.Date;
            movieView.MovieId = movieViewDTO.MovieId;
            movieView.SalonId = movieViewDTO.SalonId;

            await _iMovieViewRepository.UpdateMovieView(movieView);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ett fel inträffade vid uppdatering av filmvisningen: " + ex.Message);
        }
    }

    [HttpDelete]
    public async Task<ActionResult<MovieViewDTO>> DeleteMovieViewById(MovieViewDTO movieViewDTO)
    {
        try
        {
            var movieView = new MovieView()
            {
                MovieViewId = movieViewDTO.MovieViewId,
                Date = movieViewDTO.Date,
                MovieId = movieViewDTO.MovieId,
                SalonId = movieViewDTO.SalonId
            };
            var deletedMovie = await _iMovieViewRepository.DeleteMovieViewById(movieView);

            var deletedMovieviewDTO = new MovieViewDTO()
            {
                MovieViewId = deletedMovie.MovieViewId,
                Date = deletedMovie.Date,
                MovieId = deletedMovie.MovieId,
                SalonId = deletedMovie.SalonId
            };
            return Ok(deletedMovieviewDTO);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Ett fel uppstod när filmvisningen skulle tas bort: {ex.Message}");
        }
    }

}