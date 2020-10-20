using System;
using System.ComponentModel.DataAnnotations;

namespace MusicBox.Models
{
    public class Notification
    {
        public int Id { get; private set; }
        public DateTime DateTime { get; private set; }
        public NotificationType Type { get; private set; }
        public DateTime? OriginalDateTime { get; private set; }
        public string OriginalAddress { get; private set; }

        [Required]
        public Event Event { get; private set; }

        protected Notification()
        {
        }

        private Notification(NotificationType type, Event myEvent)
        {
            if (myEvent==null)
            {
                throw new ArgumentException("event");
            }

            Type = type;
            Event = myEvent;
            DateTime=DateTime.Now;
        }

        public static Notification EventCreated(Event myEvent)
        {
            return new Notification(NotificationType.EventCreated,myEvent);
        }

        public static Notification EventUpdated(Event NewEvent,DateTime originalDateTime, string originalAddress)
        {
            var notification= new Notification(NotificationType.EventUpdated, NewEvent);
            notification.OriginalDateTime = originalDateTime;
            notification.OriginalAddress = originalAddress;

            return notification;
        }

        public static Notification EventCanceled(Event myEvent)
        {
            return new Notification(NotificationType.EventCancelled,myEvent);
        }
    }
}