using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace MusicBox.Models
{
    public class Event
    {
        public int Id { get; set; }

        public bool IsCancelled { get; private set; }
        
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

        public ICollection<Attendance> Attendences { get; private set; }

        public Event()
        {
            Attendences = new Collection<Attendance>();
        }

        public void Cancel()
        {
            IsCancelled = true;

            var notification =Notification.EventCanceled(this);

            foreach (var attendee in Attendences.Select(x => x.Attendee))
            {
                attendee.Notify(notification);
            }
        }

        public void Modify(DateTime dateTime, string eventItemAddress, int eventItemGenre)
        {
            var notification = Notification.EventUpdated(this,DateTime,Address);
            
            Address = eventItemAddress;
            DateTime = dateTime;
            GenreId = eventItemGenre;

            foreach (var attendee in Attendences.Select(x=>x.Attendee))
            {
                attendee.Notify(notification);
            }

        }
    }
}