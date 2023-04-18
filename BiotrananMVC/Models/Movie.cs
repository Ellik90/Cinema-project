namespace BiotrananMVC.Models;
public class Movie
{
    public int MovieId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int MaxViews { get; set; }
    public decimal MoviePrice { get; set; }
    public string Language { get; set; }
    public int MovieLength { get; set; }
    public string MovieLengthString { get; set; }
    public int YearOfPublished { get; set; }
    public List<MovieView> movieViews { get; set; }
    public string Directors { get; set; }
    public string Actors { get; set; }
    public string ImageLink { get; set; }

    public Movie(string title, string description, string language, int movieLength, int maxViews, decimal maxPrice, string directors, string actors, string imageLink, int yearOfPublished)
    {
        Title = title;
        Description = description;
        Language = language;
        MovieLength = movieLength;
        MaxViews = maxViews;
        MoviePrice = MoviePrice;
        Directors = directors;
        Actors = actors;
        MovieLengthString = ConvertToHoursAndMinutes(movieLength);
        ImageLink = imageLink;
        YearOfPublished = yearOfPublished;

    }
    public Movie() { }
    public static string ConvertToHoursAndMinutes(int movielength)
    {
        var hours = movielength / 60;
        var minutes = movielength % 60;
        return $"{hours}h {minutes}min";
    }
}