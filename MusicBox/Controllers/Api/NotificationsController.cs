using AutoMapper;
using Microsoft.AspNet.Identity;
using MusicBox.Dtos;
using MusicBox.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;

namespace MusicBox.Controllers.Api
{
    [Authorize]
    public class NotificationsController : ApiController
    {
        private readonly ApplicationDbContext _context;

        public NotificationsController()
        {
            _context = new ApplicationDbContext();
            
        }

        public IEnumerable<NotificationDto> GetNotifications()
        {
            var userId=User.Identity.GetUserId();
            var notifications = _context.UserNotifications
                .Where(u => u.UserId == userId && !u.IsRead)
                .Select(x => x.Notification)
                .Include(y => y.Event.Performer)
                .ToList();

            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<ApplicationUser, UserDto>();
                cfg.CreateMap<Event, EventDto>();
                cfg.CreateMap<Notification, NotificationDto>();
            });

            IMapper mapper = config.CreateMapper();

            return notifications.Select(mapper.Map<Notification,NotificationDto>);
        }

        [HttpPost]
        public IHttpActionResult MarkAsRead()
        {
            var userId = User.Identity.GetUserId();
            var notifications = _context.UserNotifications
                .Where(u => u.UserId == userId && !u.IsRead)
                .ToList();

            notifications.ForEach(x=>x.Read());

            _context.SaveChanges();

            return Ok();
        }
    }
}
