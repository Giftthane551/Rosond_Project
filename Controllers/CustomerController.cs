using Microsoft.AspNetCore.Mvc;
using Rosond_Project.Models;

namespace Rosond_Project.Controllers
{
    public class CustomerController : Controller
    {
        private readonly LawnMowingServiceDbContext _context;

        public CustomerController(LawnMowingServiceDbContext context)
        {
            _context = context;
        }

        // GET: Display the booking form
        [HttpGet]
        public IActionResult BookMachine()
        {
            ViewBag.Machines = GetMachines(); // Fetch the list of machines
            return View(new Booking()); // Return the BookMachine view
        }

        // POST: Handle the booking submission
        [HttpPost]
        public IActionResult BookMachine(Booking booking)
        {
            if (ModelState.IsValid)
            {
                // Save the booking to the database
                _context.Bookings.Add(booking);
                _context.SaveChanges();

                // Redirect to the confirmation view
                return RedirectToAction("BookingConfirmed");
            }

            ViewBag.Machines = GetMachines(); // Repopulate machines if there are validation errors
            return View(booking); // Return the form with validation errors
        }

        // GET: Show booking confirmation
        public IActionResult BookingConfirmed()
        {
            return View(); // Return the confirmation view
        }

        // Simulated method to fetch machines
        private List<Machine> GetMachines()
        {
            return new List<Machine>
            {
                new Machine { MachineId = 1, MachineName = "Lawn Mower" },
                new Machine { MachineId = 2, MachineName = "Tractor" },
                // Add more machines as needed
            };
        }
    }
}
