using Microsoft.AspNetCore.Mvc;
using API.Models;
using API.DTO;
using API.Interface;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class MovieController : ControllerBase
{
    IMovieRepository _iMovieRepository;
    public MovieController(IMovieRepository iMovieRepository)
    {
        _iMovieRepository = iMovieRepository;
    }

    [HttpGet]
    public async Task<ActionResult<List<MovieDTO>>> GetMovies()
    {
        try
        {
            var movies = await _iMovieRepository.GetMovies();
            if (movies == null)
            {
                return Ok(new List<MovieDTO>());
            }
            var movieDtos = movies.Select(m => new MovieDTO
            {
                MovieId = m.MovieId,
                Title = m.Title,
                Description = m.Description,
                YearOfPublished = m.YearOfPublished,
                MaxViews = m.MaxViews,
                MovieLength = m.MovieLength,
                MoviePrice = m.MoviePrice,
                Language = m.Language,
                Directors = m.Directors,
                Actors = m.Actors,
                ImageLink = m.ImageLink
            }).ToList();
            return Ok(movieDtos);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpGet("movieId")]
    public async Task<ActionResult<MovieDTO>> GetMovieById(int movieId)
    {
        try
        {
            var oneMovie = await _iMovieRepository.GetMovieById(movieId);
            if (oneMovie == null)
            {
                return NotFound();
            }
            var movieDto = new MovieDTO
            {
                Title = oneMovie.Title,
                MovieLength = oneMovie.MovieLength,
                Language = oneMovie.Language,
                MoviePrice = oneMovie.MoviePrice,
                Directors = oneMovie.Directors,
                Actors = oneMovie.Actors,
                ImageLink = oneMovie.ImageLink
            };
            return Ok(movieDto);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult<MovieDTO>> CreateMovie(MovieDTO movieDto)
    {
        try
        {
            if (movieDto == null)
            {
                return BadRequest("Det finns ingen movieDTO");
            }
            var movie = new Movie
            {
                Title = movieDto.Title,
                Description = movieDto.Description,
                MovieLength = movieDto.MovieLength,
                Language = movieDto.Language,
                MaxViews = movieDto.MaxViews,
                MoviePrice = movieDto.MoviePrice,
                Directors = movieDto.Directors,
                Actors = movieDto.Actors,
                ImageLink = movieDto.ImageLink
            };

            await _iMovieRepository.CreateMovie(movie);
            return Ok(movieDto);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpPut]
    public async Task<ActionResult<MovieDTO>> UpdateMovie(MovieDTO movieDto)
    {
        try
        {
            var movie = await _iMovieRepository.GetMovieById(movieDto.MovieId);
            if (movie == null)
            {
                return NotFound($"Filmen med Id {movieDto.MovieId} hittades inte.");
            }
            movie.Title = movieDto?.Title;
            movie.MaxViews = movieDto.MaxViews;
            movie.YearOfPublished = movieDto.YearOfPublished;
            movie.Description = movieDto.Description;
            movie.MovieLength = movieDto.MovieLength;
            movie.Language = movieDto.Language;
            movie.MoviePrice = movieDto.MoviePrice;
            movie.Directors = movieDto.Directors;
            movie.Actors = movieDto.Actors;
            movie.ImageLink = movieDto.ImageLink;

            await _iMovieRepository.UpdateMovie(movie);
            return Ok(movieDto);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpDelete]
    public async Task<ActionResult<MovieDTO>> DeleteMovieById(MovieDTO movieDTO)
    {
        try
        {
            var movie = new Movie()
            {
                MovieId = movieDTO.MovieId,
                Title = movieDTO.Title,
                Language = movieDTO.Language,
                MovieLength = movieDTO.MovieLength
            };
            var deletedMovie = await _iMovieRepository.DeleteMovie(movie);

            if (deletedMovie == null)
            {
                return NotFound();
            }

            var deletedMovieDTO = new MovieDTO()
            {
                MovieId = deletedMovie.MovieId,
                Title = deletedMovie.Title,
                Language = deletedMovie.Language,
                MovieLength = deletedMovie.MovieLength
            };
            return Ok(deletedMovieDTO);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpDelete("DeleteAll")]
    public async Task<IActionResult> DeleteAll()
    {
        try
        {
            await _iMovieRepository.DeleteAll();
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
}