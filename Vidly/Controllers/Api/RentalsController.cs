using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    public class RentalsController : ApiController
    {
        private ApplicationDbContext _context = null;

        public RentalsController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        [HttpPost]
        [Authorize(Roles = RoleName.CAN_MANAGE_MOVIES)]
        public IHttpActionResult CreateNewRental(NewRentalDto rentalDto)
        {
            if (!ModelState.IsValid) {
                return BadRequest();
            }

            Customer customer = _context.Customers.Single(c => c.Id == rentalDto.CustomerId);
            List<Movie> movies = _context.Movies.Where(m => rentalDto.MovieIds.Contains(m.Id)).ToList();

            foreach (Movie movie in movies)
            {
                if (movie.NumberAvailable <= 0)
                {
                    return BadRequest("Movie is not available.");
                }

                movie.NumberAvailable--;
                var rental = new Rental()
                {
                    Customer = customer,
                    Movie = movie,
                    DateRented = DateTime.Now
                };

                _context.Rentals.Add(rental);
            }

            _context.SaveChanges();
            return Ok();
        }
    }
}
