using imovi.Models;
using imovi.Models.MovieModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace imovi.Controllers {
    public class HomeController : Controller {
        private readonly ILogger<HomeController> _logger;
        private UserContext db;
        HttpClient client;
        public HomeController(ILogger<HomeController> logger, UserContext context) {
            _logger = logger;
            client = new HttpClient();
            db = context;
        }

        public IActionResult Index() {
            //return Content(User.Identity.Name);

            ViewBag.username = User.Identity.Name;
            var response = client.
                GetStringAsync("https://api.themoviedb.org/3/list/1?api_key=30c4ec1f7ead936d610a56b54bc4bbd4");
            var data = response.Result;

            Root list = JsonConvert.DeserializeObject<Root>(data);
            return View(list.items);
        }

        public IActionResult Popular() {
            ViewBag.username = User.Identity.Name;
            var response = client.
                GetStringAsync("https://api.themoviedb.org/3/movie/popular?api_key=30c4ec1f7ead936d610a56b54bc4bbd4");
            var data = response.Result;

            Root list = JsonConvert.DeserializeObject<Root>(data);
            return View("Index", list.results);
        }

        [HttpGet]
        public IActionResult Detail(int id) {
            ViewBag.username = User.Identity.Name;
            //get movie detail
            var response = client.
                GetStringAsync("https://api.themoviedb.org/3/movie/"
                + id
                +"?api_key=30c4ec1f7ead936d610a56b54bc4bbd4");
            var data = response.Result;
            ViewBag.str = data;
            MovieDetail movie = JsonConvert.DeserializeObject<MovieDetail>(data);
            //get video key
            response = client.
                GetStringAsync(
                "https://api.themoviedb.org/3/movie/" + id + "/videos?api_key=30c4ec1f7ead936d610a56b54bc4bbd4&language=en-US");

            data = response.Result;
            VideoRoot vr = JsonConvert.DeserializeObject<VideoRoot>(data);
            if(vr.results.Count!=0)
                ViewBag.videoKey = vr.results.FirstOrDefault().key;
            else
                ViewBag.videoKey = "";
            ViewBag.bgImage = "https://image.tmdb.org/t/p/w500/" + movie.poster_path;
            return View(movie);
        }

        public IActionResult Trending() {
            ViewBag.username = User.Identity.Name;
            var response = client.
                GetStringAsync("https://api.themoviedb.org/3/trending/all/day?api_key=30c4ec1f7ead936d610a56b54bc4bbd4");
            var data = response.Result;

            Root list = JsonConvert.DeserializeObject<Root>(data);
            return View("Index", list.results);
        }

        public IActionResult TopRated() {
            ViewBag.username = User.Identity.Name;
            var response = client.
                GetStringAsync("https://api.themoviedb.org/3/movie/top_rated?api_key=30c4ec1f7ead936d610a56b54bc4bbd4");
            var data = response.Result;

            Root list = JsonConvert.DeserializeObject<Root>(data);
            return View("Index", list.results);
        }

        public IActionResult Upcoming() {
            ViewBag.username = User.Identity.Name;
            var response = client.
                GetStringAsync("https://api.themoviedb.org/3/movie/upcoming?api_key=30c4ec1f7ead936d610a56b54bc4bbd4");
            var data = response.Result;

            Root list = JsonConvert.DeserializeObject<Root>(data);
            return View("Index", list.results);
        }

        public IActionResult Latest() {
            ViewBag.username = User.Identity.Name;
            var response = client.
                GetStringAsync("https://api.themoviedb.org/3/movie/latest?api_key=30c4ec1f7ead936d610a56b54bc4bbd4");
            var data = response.Result;

            Movie movie = JsonConvert.DeserializeObject<Movie>(data);
            List<Movie> movies = new List<Movie>();
            movies.Add(movie);
            return View("Index", movies);
        }

        public IActionResult Privacy() {
            ViewBag.username = User.Identity.Name;
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            ViewBag.username = User.Identity.Name;
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Authorize]
        [HttpPost]
        public async Task<RedirectToActionResult> AddToFavourites(Movie movie) {
            ViewBag.username = User.Identity.Name;
            User user = await db.Users.FirstOrDefaultAsync(u => u.Email == User.Identity.Name);
            db.UsersMovies.Add(new User_Movie {
                user=user,
                movie_id = movie.id
            });
            db.SaveChanges();

            return RedirectToAction("Detail", new { id = movie.id });
        }
    }
}
