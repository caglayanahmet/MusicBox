using MusicBox.Models;
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

        public ActionResult Index()
        {
            var upcomingEvents = _context.Events
                .Include(x => x.Performer)
                .Where(x => x.DateTime > DateTime.Now)
                .ToList();

            return View(upcomingEvents);
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