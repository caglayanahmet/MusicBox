using Microsoft.AspNet.Identity;
using MusicBox.Dtos;
using MusicBox.Models;
using System.Linq;
using System.Web.Http;

namespace MusicBox.Controllers
{
    [Authorize]
    public class FollowingsController : ApiController
    {
        private readonly ApplicationDbContext _context;

        public FollowingsController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult Follow(FollowingDto dto)
        {
            var userId = User.Identity.GetUserId();

            if (_context.Followings.Any(x => x.FolloweeId == userId && x.FolloweeId == dto.FolloweeId))
                return BadRequest("Already a follower");

            var following = new Following()
            {
                FollowerId = userId,
                FolloweeId = dto.FolloweeId
            };

            _context.Followings.Add(following);
            _context.SaveChanges();

            return Ok();
        }
    }
}
