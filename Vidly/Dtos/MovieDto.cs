using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Vidly.Models;

namespace Vidly.Dtos
{
    public class MovieDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public GenreMovieDto GenreMovie { get; set; }

        [Required]
        public short GenreMovieId { get; set; }

        [Required]
        public DateTime ReleaseDate { get; set; }
        
        public DateTime DateAdded { get; set; }

        [Required]
        [Range(minimum: 1, maximum: 20, ErrorMessage = "El cantidad de peliculas en stock debe estar entre 1 y 20.")]
        public short NumberInStock { get; set; }
    }
}