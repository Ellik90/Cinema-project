using API.Models;
using Newtonsoft.Json;
namespace API.DTO;
public class MovieDTO
{
    public int MovieId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public decimal MoviePrice { get; set; }
    public int MaxViews { get; set; }
    public string Language { get; set; }
    public int MovieLength { get; set; }
    public int YearOfPublished { get; set; }
    public string ImageLink { get; set; }

    [JsonIgnore]
    public List<MovieView> views;
    public string Directors { get; set; }
    public string Actors { get; set; }
 
    public MovieDTO(int movieId, string title, string description, decimal moviePrice, string language, int movieLength, Movie movie, int maxViews, string directors, string actors, int yearOfPublished, string imageLink)
    {
        MovieId = movieId;
        Title = title;
        Description = description;
        MoviePrice = moviePrice;
        Language = language;
        MovieLength = movieLength;
        MaxViews = maxViews;
        Directors = directors;
        Actors = actors;
        YearOfPublished = yearOfPublished;
        ImageLink = imageLink;
    }
    public MovieDTO() { }
}
