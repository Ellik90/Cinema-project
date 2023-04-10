using API.Models;
using Newtonsoft.Json;
namespace API.DTO;
public class MovieDTO
{
    public int MovieId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int MovieViewsShown { get; set; }
    public int MaxViews { get; set; }
    public string Language { get; set; }
    public int MovieLength { get; set; }

    public int YearOfPublished { get; set; }

    [JsonIgnore]
    public List<MovieView> views;

    public string Directors { get; set; }

    public string Actors { get; set; } 
    // public Movie OneMovie { get; set; }

    public MovieDTO(int movieId, string title, string description, string language, int movieLength, Movie movie, int maxViews, string directors, string actors, int yearOfPublished)
    {
        MovieId = movieId;
        Title = title;
        Description = description;
        Language = language;
        MovieLength = movieLength;
        MaxViews = maxViews;
        Directors = directors;
        Actors = actors;
        YearOfPublished = yearOfPublished;

        // OneMovie = oneMovie;

    }
    public MovieDTO() { }
}