using Microsoft.AspNet.Identity;
using MusicBox.Models;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;

namespace MusicBox.Controllers.Api
{
    [Authorize]
    public class EventsController : ApiController
    {
        private readonly ApplicationDbContext _context;

        public EventsController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpDelete]
        public IHttpActionResult Cancel(int id)
        {
            var userId = User.Identity.GetUserId();

            var myEvent = _context.Events
                .Include(x=>x.Attendences.Select(y=>y.Attendee))
                .Single(x => x.Id == id && x.PerformerId == userId);

            if (myEvent.IsCancelled)
                return NotFound();

            myEvent.Cancel(); 
            
            _context.SaveChanges();

            return Ok();
        }
    }
}
