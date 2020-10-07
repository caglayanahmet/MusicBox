using Microsoft.AspNet.Identity;
using MusicBox.Models;
using MusicBox.ViewModels;
using System.Linq;
using System.Web.Mvc;

namespace MusicBox.Controllers
{
    public class EventsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EventsController()
        {
            _context = new ApplicationDbContext();
        }

        [Authorize]
        public ActionResult Create()
        {
            var eventsViewModelItem = new EventFormViewModel()
            {
                Genres = _context.Genres.ToList()

            };

            return View(eventsViewModelItem);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EventFormViewModel eventItem)
        {
            if (!ModelState.IsValid)
            {
                eventItem.Genres = _context.Genres.ToList();
                return View("Create", eventItem);
            }

            var item = new Event()
            {
                PerformerId = User.Identity.GetUserId(),
                Address = eventItem.Address,
                DateTime = eventItem.GetDateTime(),
                GenreId = eventItem.Genre
            };

            _context.Events.Add(item);
            _context.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
    }
}