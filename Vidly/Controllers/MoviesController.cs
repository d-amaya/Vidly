using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        private ApplicationDbContext _context = null;

        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        [HttpGet]
        public ActionResult Index()
        {
            if (User.IsInRole(RoleName.CAN_MANAGE_MOVIES))
            {
                return View("List");
            }
            return View("ReadOnlyList");
        }

        [HttpGet]
        [Authorize(Roles = RoleName.CAN_MANAGE_MOVIES)]
        public ActionResult New()
        {
            MovieFormViewModel viewModel = new MovieFormViewModel()
            {
                Movie = new Movie(),
                Genres = _context.Genres.ToList()
            };
            return View("MovieForm", viewModel);
        }

        [HttpGet]
        [Authorize(Roles = RoleName.CAN_MANAGE_MOVIES)]
        public ActionResult Edit(int id)
        {
            Movie movie = _context.Movies.Include(m => m.GenreMovie).FirstOrDefault(m => m.Id == id);
            if (movie == null)
            {
                return HttpNotFound();
            }

            MovieFormViewModel viewModel = new MovieFormViewModel()
            {
                Movie = movie,
                Genres = _context.Genres.ToList()
            };

            return View("MovieForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleName.CAN_MANAGE_MOVIES)]
        public ActionResult Save(Movie movie)
        {
            if (!ModelState.IsValid)
            {
                MovieFormViewModel viewModel = new MovieFormViewModel()
                {
                    Movie = movie,
                    Genres = _context.Genres.ToList()
                };
                return View("MovieForm", viewModel);
            }

            var movieInDB = _context.Movies.FirstOrDefault(m => m.Id == movie.Id);
            if (movieInDB == null)
            {
                movie.DateAdded = DateTime.Now;
                _context.Movies.Add(movie);
            }
            else
            {
                movieInDB.Name = movie.Name;
                movieInDB.GenreMovieId = movie.GenreMovieId;
                movieInDB.NumberInStock = movie.NumberInStock;
                movieInDB.ReleaseDate = movie.ReleaseDate;
            }
            try
            {
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return RedirectToAction("Index", "Movies");
        }
    }
}