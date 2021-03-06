﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    public class MoviesController : ApiController
    {

        ApplicationDbContext _context = null;

        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        [HttpGet]
        public IHttpActionResult GetMovies(string query = null)
        {
            try
            {
                IQueryable<Movie> moviesQuery = _context.Movies.Include(m => m.GenreMovie).Where(m => m.NumberAvailable > 0);
                if (!string.IsNullOrWhiteSpace(query))
                {
                    moviesQuery = moviesQuery.Where(m => m.Name.Contains(query));
                }
                return Ok(moviesQuery.ToList().Select(Mapper.Map<Movie, MovieDto>));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return InternalServerError();
            }
        }

        [HttpGet]
        public IHttpActionResult GetMovie(int id)
        {
            try
            {
                Movie movie = _context.Movies.SingleOrDefault(m => m.Id == id);
                if (movie == null)
                {
                    return NotFound();
                }
                return Ok(Mapper.Map<Movie, MovieDto>(movie));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return InternalServerError();
            }
        }

        [HttpPost]
        [Authorize(Roles = RoleName.CAN_MANAGE_MOVIES)]
        public IHttpActionResult CreateMovie(MovieDto movieDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                Movie movie = Mapper.Map<MovieDto, Movie>(movieDto);
                _context.Movies.Add(movie);
                _context.SaveChanges();

                movieDto.Id = movie.Id;

                return Created(new Uri(Request.RequestUri + "/" + movie.Id), movieDto);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return InternalServerError();
            }
        }

        [HttpPut]
        [Authorize(Roles = RoleName.CAN_MANAGE_MOVIES)]
        public void UpdateMovie(int id, MovieDto movieDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    BadRequest();

                Movie movie = _context.Movies.SingleOrDefault(m => m.Id == id);
                if (movie == null)
                    NotFound();

                Mapper.Map(movieDto, movie);
                _context.SaveChanges();

                Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                InternalServerError();
            }
        }

        [HttpDelete]
        [Authorize(Roles = RoleName.CAN_MANAGE_MOVIES)]
        public IHttpActionResult DeleteMovie(int id)
        {
            try
            {
                Movie movie = _context.Movies.SingleOrDefault(m => m.Id == id);
                if (movie == null)
                    return NotFound();

                _context.Movies.Remove(movie);
                _context.SaveChanges();

                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return InternalServerError();
            }
        }
    }
}
