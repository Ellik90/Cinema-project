using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using Newtonsoft.Json;

namespace BiotrananMVC.Models;

public class MovieService
{

    public readonly HttpClient _client = new HttpClient();

    public async Task<List<Movie>> GetMoviesFromApi()
    {
        try
        {
            var response = await _client.GetFromJsonAsync<List<Movie>>("https://localhost:7146/Movie");
            return response;
        }
        catch (Exception)
        {
            throw new Exception();
        }
    }

    public async Task<List<MovieView>> GetMovieViewsFromApi()
    {
        try
        {
            var movieViews = await _client.GetFromJsonAsync<List<MovieView>>("https://localhost:7146/MovieView/getUpcomingViews");
            return movieViews;
        }
        catch(Exception)
        {
            throw new Exception();
        }
    }


    // public async Task<List<Movie>> GetMoviesAsync()
    // {
    //     var response = await _client.GetAsync("api/movies");
    //     if (response.IsSuccessStatusCode)
    //     {
    //         var content = await response.Content.ReadAsStringAsync();
    //         return JsonConvert.DeserializeObject<List<Movie>>(content);
    //     }

    //     return null;
    // }



}

