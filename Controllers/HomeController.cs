using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
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
                .OrderByDescending(x => x.NumVotes)
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

        [HttpGet]
        public IActionResult Edit(int movieid)
        {
            ViewBag.Categories = MovieContext.Categories.ToList();

            var movie = MovieContext.responses.Single(x => x.MovieId == movieid);

            return View("AddMovie", movie);
        }

        [HttpPost]
        public IActionResult Edit(Movie bruh)
        {
            MovieContext.Update(bruh);
            MovieContext.SaveChanges();

            return RedirectToAction("Index");
        }



        [HttpPost]
        public IActionResult Delete(Movie ar)
        {
            MovieContext.responses.Remove(ar);
            MovieContext.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Vote(int movieid)
        {
            MovieContext.responses.Single(x => x.MovieId == movieid).NumVotes += 1;
            MovieContext.SaveChanges();
            return RedirectToAction("Index");
        }

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
    }
}
