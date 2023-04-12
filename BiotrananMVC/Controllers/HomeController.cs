using System.Linq;
using System.Threading.Tasks;
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

        public async Task<IActionResult> MovieReservation(int movieViewId)
        {
            Console.WriteLine("movie reservation anropas och movieviewid = " + movieViewId);
            var reservation = new Reservation();
            reservation.MovieViewId = movieViewId;
            return View(reservation);
        }

        public async Task<IActionResult> BookedReservations(Reservation reservation)
        {
            await _movieService.NewReservationToApi(reservation);
            return View();
        }


        // public async Task<IActionResult> CreateNewReservation(Reservation reservation)
        // {
        //     bool     = await _movieService.CheckAvailableSeats(reservation.MovieId, reservation.SeatCount);

        //     if (enoughSeatsAvailable)
        //     {
        //         await _movieService.AddReservation(reservation);
        //         return RedirectToAction("Index");
        //     }
        //     else
        //     {
        //         return BadRequest("Not enough available seats");
        //     }
        // }

        public async Task<IActionResult> MovieDetails(int id)
        {
            var movies = await _movieService.GetMoviesFromApi();
            Movie movie = null;
            foreach (var m in movies)
            {
                if (m.MovieId == id)
                {
                    movie = m;
                    break;
                }
            }
            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
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

