using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        private readonly List<Movie> movies = new List<Movie>()
        {
            new Movie() { Id = 1, Name = "Movie Number 1" },
            new Movie() { Id = 2, Name = "Movies Number 2" }
        };

        public ActionResult Index()
        {
            MovieViewModel movieViewModel = new MovieViewModel() { Movies = movies };
            return View(movieViewModel);
        }

        [Route("Movies/{id}")]
        public ActionResult Movies(int id)
        {
            foreach (var m in movies)
            {
                if (m.Id == id)
                {
                    return View(m);
                }
            }

            return HttpNotFound();
        }
    }
}