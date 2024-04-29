using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MoviesDB.Models
{
    public class Movies
    {
        public class Movie
        {
            [Key]
            public int Mid { get; set; }

            [Required]
            public string Moviename { get; set; }

            [Display(Name = "Release Date")]
            [DataType(DataType.Date)]
            public DateTime DateofRelease { get; set; }
        }
    }
}