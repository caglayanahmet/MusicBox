using MusicBox.Models;
using MusicBox.ViewModels;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace MusicBox.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController()
        {
            _context= new ApplicationDbContext();
        }

        public ActionResult Index(string query =null)
        {
            var upcomingEvents = _context.Events
                .Include(x => x.Performer)
                .Include(x=>x.Genre)
                .Where(x => x.DateTime > DateTime.Now && !x.IsCancelled);

            if (!String.IsNullOrWhiteSpace(query))
            {
                upcomingEvents = upcomingEvents
                    .Where(x => 
                        x.Performer.Name.Contains(query) ||
                        x.Genre.Name.Contains(query) ||
                        x.Address.Contains(query));
            }

            var viewModel = new EventsViewModel
            {
                UpcomingEvents = upcomingEvents,
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "Upcoming Events",
                SearchTerm = query
            };

            return View("Events",viewModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        
    }
}