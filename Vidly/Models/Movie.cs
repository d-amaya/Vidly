using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class Movie
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public GenreMovie GenreMovie { get; set; }

        [Required]
        [DisplayName("Genre")]
        public short GenreMovieId { get; set; }

        [Required]
        [DisplayName("Release Date")]
        public DateTime ReleaseDate { get; set; }

        [DisplayName("Date Added")]
        public DateTime DateAdded { get; set; }

        [Required]
        [DisplayName("Number In Stock")]
        public short NumberInStock { get; set; }
    }
}