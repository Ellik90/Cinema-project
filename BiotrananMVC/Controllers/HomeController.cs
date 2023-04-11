using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BiotrananMVC.Models;

namespace BiotrananMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MovieService _movieService;

        public HomeController(ILogger<HomeController> logger, MovieService movieService)
        {
            _logger = logger;
            _movieService = movieService;
        }

        public async Task<IActionResult> UpcomingViews()
        {
            var movieViews = await _movieService.GetMovieViewsFromApi();
            if (movieViews == null)
            {
                return NotFound();
            }
            return View(movieViews);
        }

        public async Task<IActionResult> Index()
        {
            var movies = await _movieService.GetMoviesFromApi();
            if (movies == null)
            {
                return NotFound(); 
            }
            return View(movies);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

