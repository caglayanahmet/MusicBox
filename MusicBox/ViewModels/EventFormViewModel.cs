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
        public string Date { get; set; }
        
        [Required]
        public string Time { get; set; }
        
        [Required]
        public int Genre { get; set; }

        public IEnumerable<Genre> Genres { get; set; }

        public DateTime DateTime
        {
            get{return DateTime.Parse(string.Format("{0} {1}",Date,Time));}
        }
    }
}