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
            try
            {
                var movieViews = await _movieService.GetMovieViewsFromApi();
                var movieView = movieViews.Find(m => m.MovieViewId == movieViewId);

                var movies = await _movieService.GetMoviesFromApi(); //by id direkt istället
                var movie = movies.Find(m => m.MovieId == movieView.MovieId);

                var reservation = new Reservation();
                reservation.MovieView = movieView;
                reservation.MovieViewId = movieViewId;
                reservation.Movie = movie;

                return View(reservation);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", new { message = ex.Message });
            }
        }

        public async Task<IActionResult> ConfirmReservation(Reservation reservation)
        {
            if (reservation == null || reservation.NumberOfSeats <= 0 || reservation.Movie == null || reservation.MovieView == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var reservationPrice = reservation.NumberOfSeats * reservation.Movie.MoviePrice;
            reservation.ReservationPrice = reservationPrice;
            return View(reservation);
        }

        public async Task<IActionResult> BookedReservations(Reservation reservation)
        {
            try
            {
                var newReservation = await _movieService.NewReservationToApi(reservation);

                var movieViews = await _movieService.GetMovieViewsFromApi();
                var movieView = movieViews.Find(m => m.MovieViewId == newReservation.MovieViewId);

                var bookedReservationViewModel = new BookedReservationViewModel(
                    newReservation.ReservationId,
                    newReservation.CustomerName,
                    newReservation.PhoneNumber,
                    newReservation.NumberOfSeats,
                    movieView.Date,
                    movieView.MovieTitle
                );
                return View(bookedReservationViewModel);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public async Task<IActionResult> MovieDetails(int movieId)
        {
            try
            {
                var movies = await _movieService.GetMoviesFromApi();
                Movie movie = null;
                foreach (var m in movies)
                {
                    if (m.MovieId == movieId)
                    {
                        movie = new(m.Title, m.Description, m.Language, m.MovieLength, m.MaxViews, m.MoviePrice, m.Directors, m.Actors);
                        break;
                    }
                }
                if (movie == null)
                {
                    return NotFound();
                }
                return View(movie);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // public async Task<IActionResult> UpcomingViews()
        // {
        //     var movieViews = await _movieService.GetMovieViewsFromApi();
        //     if (movieViews == null)
        //     {
        //         return NotFound();
        //     }
        //     return View(movieViews);
        // }

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

