namespace API.Models;
public class Movie
{
    public int MovieId { get; set; }
    public string Title { get; set; }
     public string Description { get; set; }
    public int MovieViewsShown { get; set; }
    public int MaxViews { get; set; }
    public string Language { get; set; }
    public int MovieLength { get; set; }

    public List<MovieView> views { get; set; }
    public List<string> Directors;
    public List<string> Actors;
    // public Movie OneMovie { get; set; }

    public Movie(string title, string description, string language, int movieLength)
    {
        Title = title;
        Description = description;
        Language = language;
        MovieLength = movieLength;
      
        // OneMovie = oneMovie;
       
    }
    public Movie(){}
}