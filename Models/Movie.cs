using System;
using System.ComponentModel.DataAnnotations;

namespace MovieVoting.Models
{
    public class Movie
    {
        [Key]
        [Required]
        public int MovieId { get; set; }
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        [Required(ErrorMessage = "Length is required")]
        public string Length { get; set; }
        public int NumVotes { get; set; }
        public bool Watched { get; set; }
        public bool Voting { get; set; }
        public string DateWatched { get; set; }


    }
}
