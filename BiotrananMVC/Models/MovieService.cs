using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using Newtonsoft.Json;

namespace BiotrananMVC.Models;

public class MovieService
{

    public readonly HttpClient _client = new HttpClient();

    public async Task<Reservation> NewReservationToApi(Reservation reservations)
    {
        try
        {
            var response = await _client.PostAsJsonAsync("https://localhost:7146/Reservation", reservations);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var reservation = JsonConvert.DeserializeObject<Reservation>(content);
            return reservation;
        }
        catch (Exception)
        {
            throw new Exception();
        }
    }


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

    //    public async Task<List<Movie>> GetMovieByIdFromApi(int id)
    // {
    //     try
    //     {
    //         var movie = await _client.GetFromJsonAsync<List<Movie>>($"https://localhost:7146/Movie/{id}");
    //         return movie;
    //     }
    //     catch (Exception)
    //     {
    //         throw new Exception();
    //     }
    // }


    public async Task<List<MovieView>> GetMovieViewsFromApi()
    {
        try
        {
            var movieViews = await _client.GetFromJsonAsync<List<MovieView>>("https://localhost:7146/MovieView/getUpcomingViews");
            return movieViews;
        }
        catch (Exception)
        {
            throw new Exception();
        }
    }



}

