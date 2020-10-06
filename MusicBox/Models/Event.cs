using System;
using System.ComponentModel.DataAnnotations;

namespace MusicBox.Models
{
    public class Event
    {
        public int Id { get; set; }

        [Required]
        public ApplicationUser Performer { get; set; }
        
        public DateTime DateTime { get; set; }
        
        [Required]
        [StringLength(255)]
        public string Address { get; set; }
        
        [Required]
        public Genre Genre { get; set; }

    }
}