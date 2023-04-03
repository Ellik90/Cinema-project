using API.Models;
using Microsoft.EntityFrameworkCore;
using API.DTO;

namespace API.Data;

public class MovieViewSeedData
{
    MyDbContext _myDbContext;

    public MovieViewSeedData(MyDbContext myDbContext)
    {
        _myDbContext = myDbContext;
    }

    // public async Task<MovieViewDTO> CreateNewMovieView(MovieViewDTO movieView)
    // {
    //     _myDbContext.MovieViews.Add(movieView);
    //      _myDbContext.movies.Include(m => m.MovieId);
    //     await _myDbContext.SaveChangesAsync();
    //     return movieView;
    // }
}