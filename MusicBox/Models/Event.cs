using System;
using System.ComponentModel.DataAnnotations;

namespace MusicBox.Models
{
    public class Event
    {
        public int Id { get; set; }

        
        public ApplicationUser Performer { get; set; }

        [Required]
        public string PerformerId { get; set; }
        
        public DateTime DateTime { get; set; }
        
        [Required]
        [StringLength(255)]
        public string Address { get; set; }
        
        
        public Genre Genre { get; set; }

        [Required]
        public int GenreId { get; set; }

    }
}