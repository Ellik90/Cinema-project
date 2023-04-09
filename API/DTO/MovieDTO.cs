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
    
    public List<string> Directors { get; set; } = new();
      
    public List<string> Actors { get; set; } = new();
    // public Movie OneMovie { get; set; }

    public MovieDTO(int movieId, string title, string description, string language, int movieLength, Movie movie)
    {
        MovieId = movieId;
        Title = title;
        Description = description;
        Language = language;
        MovieLength = movieLength;
     
        // OneMovie = oneMovie;
       
    }
    public MovieDTO(){}
}
