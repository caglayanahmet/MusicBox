using Microsoft.AspNet.Identity;
using MusicBox.Models;
using MusicBox.ViewModels;
using System;
using System.Data.Entity;
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
        public ActionResult MyEvents()
        {
            var userId = User.Identity.GetUserId();

            var events = _context.Events
                .Where(x => x.PerformerId == userId && x.DateTime > DateTime.Now && !x.IsCancelled)
                .Include(x=>x.Genre)
                .ToList();

            return View(events);
        }

        [Authorize]
        public ActionResult Attending()
        {
            var userId = User.Identity.GetUserId();

            var events = _context.Attendances
                .Where(x => x.AttendeeId == userId)
                .Select(x => x.Event)
                .Include(x=>x.Performer)
                .Include(x=>x.Genre)
                .ToList();

            var viewModel = new EventsViewModel()
            {
                UpcomingEvents = events,
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "Events I'm Attending"
                
            };

            return View("Events",viewModel);
        }


        [Authorize]
        public ActionResult Create()
        {
            var eventsViewModelItem = new EventFormViewModel()
            {
                Genres = _context.Genres.ToList(),
                Heading = "Create New Event"

            };

            return View("EventForm",eventsViewModelItem);
        }

        [Authorize]
        public ActionResult Edit(int id)
        {
            var userId = User.Identity.GetUserId();

            var myevent = _context.Events.FirstOrDefault(x => x.Id == id && x.PerformerId == userId);

            var eventsViewModelItem = new EventFormViewModel()
            {
                Genres = _context.Genres.ToList(),
                Date = myevent.DateTime.ToString("d MMM yyyy"),
                Time = myevent.DateTime.ToString("HH:mm"),
                Genre = myevent.GenreId,
                Address = myevent.Address,
                Heading = "Edit Event",
                Id = myevent.Id

            };

            return View("EventForm",eventsViewModelItem);
        }


        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EventFormViewModel eventItem)
        {
            if (!ModelState.IsValid)
            {
                eventItem.Genres = _context.Genres.ToList();
                return View("EventForm", eventItem);
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
            return RedirectToAction("MyEvents", "Events");
        } 
        
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(EventFormViewModel eventItem)
        {
            if (!ModelState.IsValid)
            {
                eventItem.Genres = _context.Genres.ToList();
                return View("EventForm", eventItem);
            }

            var userId =User.Identity.GetUserId();
            var myEvent = _context.Events
                .Include(x=>x.Attendences.Select(y=>y.Attendee))
                .FirstOrDefault(x => x.Id == eventItem.Id && x.PerformerId == userId);

            myEvent.Modify(eventItem.GetDateTime(), eventItem.Address, eventItem.Genre);

            myEvent.Address = eventItem.Address;
            myEvent.DateTime = eventItem.GetDateTime();
            myEvent.GenreId = eventItem.Genre;

            _context.SaveChanges();
            return RedirectToAction("MyEvents", "Events");
        }

        [HttpPost]
        public ActionResult Search(EventsViewModel viewModel)
        {
            return RedirectToAction("Index", "Home", new {query = viewModel.SearchTerm});
        }


    }
}