using System.Linq;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using MusicBox.Dtos;
using MusicBox.Models;

namespace MusicBox.Controllers.Api
{
    [Authorize]
    public class AttendancesController : ApiController
    {
        private readonly ApplicationDbContext _context;

        public AttendancesController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpPost  ]
        public IHttpActionResult Attend(AttendanceDto dto)
        {
            var userId = User.Identity.GetUserId();

            if (_context.Attendances.Any(x => x.AttendeeId ==userId  && x.EventId == dto.EventId))
                return BadRequest("Already attended!");

            var attendance = new Attendance
            {
                EventId = dto.EventId,
                AttendeeId = userId
            };

            _context.Attendances.Add(attendance);
            _context.SaveChanges();

            return Ok();
        }
    }
}
