namespace API.Models;
public class Movie
{
    public int MovieId { get; set; }
    public string Title { get; set; }
    public decimal MoviePrice { get; set; }
    public string Description { get; set; }
    public int MovieViewsShown { get; set; }
    public int MaxViews { get; set; }
    public string Language { get; set; }
    public int MovieLength { get; set; }
    public int YearOfPublished { get; set; }
    public List<MovieView> movieViews { get; set; }
    public string Directors { get; set; }
    public string Actors { get; set; }

    public Movie(string title, string description, string language, decimal moviePrice, int movieLenght, int maxViews, string directors, string actors)
    {
        Title = title;
        Description = description;
        Language = language;
        MoviePrice = moviePrice;
        MovieLength = movieLenght;
        MaxViews = maxViews;
        Directors = directors;
        Actors = actors;
    }
    public Movie() { }
}