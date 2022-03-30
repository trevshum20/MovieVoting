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
                new Category { CategoryId = 6, CategoryName = "Miscellaneous" },
                new Category { CategoryId = 7, CategoryName = "Romantic" },
                new Category { CategoryId = 8, CategoryName = "Thriller" }
            );

            _ = mb.Entity<Movie>().HasData(

                new Movie
                {
                    MovieId = 1,
                    CategoryId = 1,
                    Title = "Karate Kid",
                    Length = "126",
                    NumVotes = 0,
                    Watched = false,
                    Voting = true,
                    



                },

                new Movie
                {
                    MovieId = 2,
                    CategoryId = 1,
                    Title = "The Dark Knight",
                    Length = "152",
                    NumVotes = 0,
                    Watched = false,
                    Voting = true

                },

                new Movie
                {
                    MovieId = 3,
                    CategoryId = 8,
                    Title = "Dune",
                    Length = "155",
                    NumVotes = 0,
                    Watched = false,
                    Voting = true

                },

                new Movie
                {
                    MovieId = 4,
                    CategoryId = 4,
                    Title = "Far and Away",
                    Length = "124",
                    NumVotes = 0,
                    Watched = false,
                    Voting = false

                }
            );
        }
    }
}
