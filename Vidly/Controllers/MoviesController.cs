﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        // GET: Movies/Random
        public ActionResult Random()
        {
            var movie = new Movie() { Name = "Shrek!" };
            return View(movie);
        }

        public ActionResult Edit(int id)
        {
            return Content($"id= {id}");
        }

        public ActionResult Index(int? pageIndex, string sortBy)
        {
            if (!pageIndex.HasValue)
            {
                pageIndex = 1;
            }

            if (String.IsNullOrWhiteSpace(sortBy))
            {
                sortBy = "Name";
            }

            //return Content($"pageIndex={pageIndex}&sortBy={sortBy}");
            return Content(String.Format("pageIndex={0}&sortBy={1}", pageIndex, sortBy));
        }

        [Route("movies/released/{year:minlength(4):maxlength(4)}/{month:regex(\\d{2}):range(1, 12)}")]
        public ActionResult ByRealeaseDate(int year, int month)
        {
            return Content($"Date={year}/{month}");
        }
    }
}