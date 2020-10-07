using MusicBox.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MusicBox.ViewModels
{
    public class EventFormViewModel
    {
        [Required]
        public string  Address { get; set; }
        
        [Required]
        [FutureDate]
        public string Date { get; set; }
        
        [Required]
        [ValidTime]
        public string Time { get; set; }
        
        [Required]
        public int Genre { get; set; }

        public IEnumerable<Genre> Genres { get; set; }

        public DateTime GetDateTime()
        {
            return DateTime.Parse($"{Date} {Time}");
        }
    }
}