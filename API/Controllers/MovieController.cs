using Microsoft.AspNetCore.Mvc;
using API.Models;
using API.DTO;
using API.Data;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class MovieController : ControllerBase
{
    MovieRepository _movieRepository;
    public MovieController(MovieRepository movieRepository)
    {
        _movieRepository = movieRepository;
    }

    [HttpGet]
    public async Task<ActionResult<List<MovieDTO>>> GetMovies()
    {
        try
        {
            var movies = await _movieRepository.GetMovies();
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
                Actors = m.Actors
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
            var oneMovie = await _movieRepository.GetMovieById(movieId);
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
                Actors = oneMovie.Actors
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
                Actors = movieDto.Actors
            };

            await _movieRepository.CreateMovie(movie);
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
            var movie = await _movieRepository.GetMovieById(movieDto.MovieId);
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

            await _movieRepository.UpdateMovie(movie);
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
            var deletedMovie = await _movieRepository.DeleteMovie(movie);

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
            await _movieRepository.DeleteAll();
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
}