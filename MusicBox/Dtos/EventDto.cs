using System;

namespace MusicBox.Dtos
{
    public class EventDto
    {
        public int Id { get; set; }
        public bool IsCancelled { get;  set; }
        public UserDto Performer { get; set; }
        public DateTime DateTime { get; set; }
        public string Address { get; set; }
        public GenreDto Genre { get; set; }
    }
}