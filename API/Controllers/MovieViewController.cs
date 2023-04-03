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

    
    // [HttpPost]
    // public async Task<ActionResult<MovieViewDTO>> CreateNewMovieView(MovieViewDTO movieViewDTO)
    // {
    //     await _movieViewSeedData.CreateNewMovieView(movieViewDTO);
    //    return Ok(movieViewDTO);
    // }

}