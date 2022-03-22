using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MovieVoting.Models;

namespace MovieVoting.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private AddMovieContext MovieContext { get; set; }

        public HomeController(ILogger<HomeController> logger, AddMovieContext jotaro)
        {
            _logger = logger;
            MovieContext = jotaro;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var movies = MovieContext.responses
                .Include(x => x.Category)
                .Where(x => x.Voting == true)
                .OrderByDescending(x => x.NumVotes)
                .ToList();

            return View(movies);
        }

        [Authorize]
        public IActionResult Admin()
        {
            var movies = MovieContext.responses
                .Include(x => x.Category)
                .OrderBy(x => x.Title)
                .ToList();

            return View(movies);
        }

        [HttpGet]
        public IActionResult AddMovie()
        {
            ViewBag.Categories = MovieContext.Categories.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult AddMovie(Movie req)
        {
            if (ModelState.IsValid)
            {
                MovieContext.Add(req);
                MovieContext.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Categories = MovieContext.Categories.ToList();
                return View(req);
            }
        }

        public IActionResult Categories()
        {
            var categories = MovieContext.Categories.ToList();
            return View(categories);
        }

        [HttpGet]
        public IActionResult AddCategory()
        {
            
            //ViewBag.Categories = MovieContext.Categories.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult AddCategory(Category req)
        {
            if (ModelState.IsValid)
            {
                MovieContext.Add(req);
                MovieContext.SaveChanges();
                return RedirectToAction("Categories");
            }
            else
            {
                ViewBag.Categories = MovieContext.Categories.ToList();
                return View(req);
            }
        }


        [HttpGet]
        public IActionResult EditCategory(int categoryid)
        {
            //ViewBag.Categories = MovieContext.Categories.ToList();

            var category = MovieContext.Categories.Single(x => x.CategoryId == categoryid);

            return View("AddCategory", category);
        }

        
        [HttpPost]
        public IActionResult EditCategory(Category bruh)
        {
            MovieContext.Update(bruh);
            MovieContext.SaveChanges();

            return RedirectToAction("Categories");
        }

        [HttpPost]
        public IActionResult DeleteCategory(Category ar)
        {
            MovieContext.Categories.Remove(ar);
            MovieContext.SaveChanges();
            return RedirectToAction("Categories");
        }


        [Authorize]
        [HttpGet]
        public IActionResult Edit(int movieid)
        {
            ViewBag.Categories = MovieContext.Categories.ToList();

            var movie = MovieContext.responses.Single(x => x.MovieId == movieid);

            return View("Edit", movie);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Edit(Movie bruh)
        {
            MovieContext.Update(bruh);
            MovieContext.SaveChanges();

            return RedirectToAction("Admin");
        }


        [Authorize]
        [HttpPost]
        public IActionResult Delete(Movie ar)
        {
            MovieContext.responses.Remove(ar);
            MovieContext.SaveChanges();
            return RedirectToAction("Admin");
        }

        
        [HttpPost]
        public IActionResult Vote(int movieid)
        {
            MovieContext.responses.Single(x => x.MovieId == movieid).NumVotes += 1;
            MovieContext.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize]
        [HttpPost]
        public IActionResult ResetVote(int movieid)
        {
            MovieContext.responses.Single(x => x.MovieId == movieid).NumVotes = 0;
            MovieContext.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult VoteConfirmation (int movieid)
        {
            var movie = MovieContext.responses.Single(x => x.MovieId == movieid);
            return View(movie);
        }

        [HttpGet]
        public IActionResult Watched()
        {
            var movies = MovieContext.responses
                .Include(x => x.Category)
                .OrderBy(x => x.Title)
                .Where(x => x.Watched == true)
                .ToList();

            return View(movies);
        }

        [HttpGet]
        public IActionResult WatchedDate(int movieid)
        {
            var movie = MovieContext.responses.Single(x => x.MovieId == movieid);
            return View(movie);
        }

        [HttpPost]
        public IActionResult Watched(int movieid, string watchdate)
        {
            MovieContext.responses.Single(x => x.MovieId == movieid).Watched = true;
            MovieContext.responses.Single(x => x.MovieId == movieid).DateWatched = watchdate;
            MovieContext.SaveChanges();
            return RedirectToAction("Watched");
        }

        [HttpGet]
        public IActionResult WantWatch()
        {
            var movies = MovieContext.responses
                .Include(x => x.Category)
                .OrderBy(x => x.Title)
                .Where(x => x.Watched == false)
                .Where(x => x.Voting == false)
                .ToList();

            return View(movies);
        }

        [HttpPost]
        public IActionResult WantWatch(int movieid)
        {
            MovieContext.responses.Single(x => x.MovieId == movieid).Watched = false;
            MovieContext.responses.Single(x => x.MovieId == movieid).DateWatched = "";
            MovieContext.SaveChanges();
            return RedirectToAction("Watched");
        }

        [HttpPost]
        public IActionResult MoveToVote(int movieid)
        {
            MovieContext.responses.Single(x => x.MovieId == movieid).Voting = true;
            MovieContext.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize]
        [HttpPost]
        public IActionResult RemoveFromVote(int movieid)
        {
            MovieContext.responses.Single(x => x.MovieId == movieid).Voting = false;
            MovieContext.responses.Single(x => x.MovieId == movieid).NumVotes = 0;
            MovieContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
