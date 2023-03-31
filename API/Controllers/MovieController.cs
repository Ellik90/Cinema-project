using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Models;
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
    public async Task<ActionResult<List<Movie>>> GetMovies()
    {
        var movies = await _seedData.GetMovies();
        return Ok(movies);
    }

    [HttpGet("movieId")]
    public async Task<ActionResult<Movie>> GetMovieById(int movieId)
    {
        var oneMovie = await _seedData.GetMovieById(movieId);
        return Ok(oneMovie);
    }

    [HttpPost]
    public async Task<ActionResult<Movie>> CreateMovie(Movie movie)
    {
        await _seedData.CreateMovie(movie);
        return Ok(movie);
        
    }

    [HttpPut]
    public async Task<ActionResult<Movie>> UpdateMovie(Movie movie)
    {
        await _seedData.UpdateMovie(movie);
        return Ok(movie);
    }

    [HttpDelete]
    public async Task<ActionResult<Movie>> DeleteMovie(Movie movie)
    {
        await _seedData.DeleteMovie(movie);
        return Ok(movie);
    }

    [HttpDelete("DeleteAll")]
    public async Task<ActionResult<Movie>> DeleteAll()
    {
        await _seedData.DeleteAll();
        return Ok();
    }

    
}