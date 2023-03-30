namespace API.Models;
public class Movie
{
    public int Id { get; set; }
    public string Title { get; set; }
    // public int MovieViews { get; set; }
    // public int MaxViews { get; set; }
    public string Language { get; set; }
    // public TimeSpan MovieLength { get; set; }
    // public List<string> Directors { get; set; }
    // public List<string> Actors { get; set; }

    public Movie(string title, string language)
    {
        Title = title;
        Language = language;
        // MovieLength = movieLength;
       
    }
}