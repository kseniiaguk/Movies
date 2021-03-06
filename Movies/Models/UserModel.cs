﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.Models
{
    public class UserModel
    {
        [Key]
        public int Id { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string Name { get; set; }
        //[InverseProperty("User")]
        public virtual ICollection<SpecifiedMovieModel> SpecifiedMovies { get; set; }

        public UserModel()
        {
            SpecifiedMovies = new List<SpecifiedMovieModel>();
        }
    }
}
