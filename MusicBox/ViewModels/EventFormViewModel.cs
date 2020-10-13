using MusicBox.Controllers;
using MusicBox.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace MusicBox.ViewModels
{
    public class EventFormViewModel
    {
        public int  Id { get; set; }
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
        
        public string Heading { get; set; }

        public string Action
        {
            get
            {
                Expression<Func<EventsController, ActionResult>> update =
                    (x => x.Update(this));

                Expression<Func<EventsController, ActionResult>> create =
                    (x => x.Create(this));

                var action = (Id != 0) ? update : create;
                return (action.Body as MethodCallExpression).Method.Name;

                
            }
        }

        public DateTime GetDateTime()
        {
            return DateTime.Parse($"{Date} {Time}");
        }
    }
}