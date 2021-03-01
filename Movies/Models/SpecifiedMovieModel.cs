using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.Models
{
    public class SpecifiedMovieModel
    {
        [Key]
        public int Id { get; set; }
        public string Link { get; set; }
        public string Comment { get; set; }
        public virtual CommonMovieModel CommonMovie { get; set; }
        //public int? UserId { get; set; }
        //[ForeignKey("SpecifiedMovies")]
        public virtual UserModel User { get; set; }
    }
}
