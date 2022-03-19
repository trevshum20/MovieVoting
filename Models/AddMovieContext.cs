using System;
using Microsoft.EntityFrameworkCore;

namespace MovieVoting.Models
{
    public class AddMovieContext : DbContext
    {


        public AddMovieContext(DbContextOptions<AddMovieContext> options) : base(options)
        {

        }

        public DbSet<Movie> responses { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder mb)
        {
            mb.Entity<Category>().HasData(
                new Category { CategoryId = 1, CategoryName = "Action / Adventure" },
                new Category { CategoryId = 2, CategoryName = "Comedy" },
                new Category { CategoryId = 3, CategoryName = "Drama" },
                new Category { CategoryId = 4, CategoryName = "Family" },
                new Category { CategoryId = 5, CategoryName = "Horror / Suspense" },
                new Category { CategoryId = 6, CategoryName = "Miscellaneious" },
                new Category { CategoryId = 7, CategoryName = "Romantic" },
                new Category { CategoryId = 8, CategoryName = "Thriller" }
            );
                
        }
    }
}
