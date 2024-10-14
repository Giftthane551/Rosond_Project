using Microsoft.AspNetCore.Mvc;
using Rosond_Project.Models;

namespace Rosond_Project.Controllers
{
    public class OperatorController : Controller
    {
        private readonly LawnMowingServiceDbContext _context;

        public OperatorController(LawnMowingServiceDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult AcknowledgeBooking(int operatorId)
        {
            var bookings = _context.Bookings
                .Where(b => b.OperatorId == operatorId && b.Status == "Confirmed")
                .ToList();
            return View(bookings);
        }

        [HttpPost]
        public ActionResult AcknowledgeBookingPost(int bookingId)
        {
            var booking = _context.Bookings.Find(bookingId);
            if (booking != null)
            {
                booking.Status = "Acknowledged";
                _context.SaveChanges();
            }
            return RedirectToAction("OperatorDashboard");
        }
    }
}
