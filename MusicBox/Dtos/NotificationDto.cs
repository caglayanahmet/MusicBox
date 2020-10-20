using System;
using MusicBox.Models;

namespace MusicBox.Dtos
{
    public class NotificationDto
    {
        public DateTime DateTime { get; set; }
        public NotificationType Type { get; set; }
        public DateTime? OriginalDateTime { get;  set; }
        public string OriginalAddress { get; set; }
        public EventDto Event { get; set; }
    }
}