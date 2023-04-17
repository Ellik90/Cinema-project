using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Models;
using API.DTO;
using API.Data;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class MovieController : ControllerBase
{
    SeedData _seedData;
    public MovieController(SeedData seedData)
    {
        _seedData = seedData;
    }

    [HttpGet]
    public async Task<ActionResult<List<MovieDTO>>> GetMovies()
    {
        var movies = await _seedData.GetMovies();
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

    [HttpGet("movieId")]
    public async Task<ActionResult<MovieDTO>> GetMovieById(int movieId) // ÄNDRADE DENNA, KOLLA SÅ DET STÄMMER
    {
        var oneMovie = await _seedData.GetMovieById(movieId);

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

    [HttpPost]
    public async Task<ActionResult<MovieDTO>> CreateMovie(MovieDTO movieDto)
    {
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

        await _seedData.CreateMovie(movie);
        return Ok(movieDto);
    }

    [HttpPut]
    public async Task<ActionResult<MovieDTO>> UpdateMovie(MovieDTO movieDto)
    {
        var movie = await _seedData.GetMovieById(movieDto.MovieId);
        movie.Title = movieDto?.Title;
        movie.MaxViews = movieDto.MaxViews;
        movie.YearOfPublished = movieDto.YearOfPublished;
        movie.Description = movieDto.Description;
        movie.MovieLength = movieDto.MovieLength;
        movie.Language = movieDto.Language;
        movie.MoviePrice = movieDto.MoviePrice;
        movie.Directors = movieDto.Directors;
        movie.Actors = movieDto.Actors;

        await _seedData.UpdateMovie(movie);
        return Ok(movieDto);
    }

    [HttpDelete]
    public async Task<ActionResult<MovieDTO>> DeleteMovieById(MovieDTO movieDTO)
    {
        var movie = new Movie()
        {
            MovieId = movieDTO.MovieId,
            Title = movieDTO.Title,
            Language = movieDTO.Language,
            MovieLength = movieDTO.MovieLength
        };
        var deletedMovie = await _seedData.DeleteMovie(movie);

        var deletedMovieDTO = new MovieDTO()
        {
            MovieId = deletedMovie.MovieId,
            Title = deletedMovie.Title,
            Language = deletedMovie.Language,
            MovieLength = deletedMovie.MovieLength

        };
        return Ok(deletedMovieDTO);
    }

    [HttpDelete("DeleteAll")]
    public async Task<ActionResult<MovieDTO>> DeleteAll()
    {
        await _seedData.DeleteAll();
        return Ok();
    }
}