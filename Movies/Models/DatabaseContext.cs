using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Movies.Models.ViewModels;

namespace Movies.Models
{
    public class DatabaseContext : DbContext
    {
        public DbSet<UserModel> Users { get; set; }
        public DbSet<CommonMovieModel> CommonMovies { get; set; }
        public DbSet<SpecifiedMovieModel> SpecifiedMovies { get; set; }
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) => Database.EnsureCreated();
    }
}
