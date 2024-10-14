using Microsoft.AspNetCore.Mvc;
using Rosond_Project.Models;

namespace Rosond_Project.Controllers
{
    public class ConflictManagerController : Controller
    {
        private readonly LawnMowingServiceDbContext _context;

        public ConflictManagerController(LawnMowingServiceDbContext context)
        {
            _context = context;
        }

        public ActionResult ResolveConflict(int machineId, DateTime bookingDate)
        {
            // Get original machine and any available alternatives
            var originalMachine = _context.Machines.Find(machineId);
            if (originalMachine == null)
            {
                // Handle the case when the original machine is not found
                return NotFound(); // or redirect to an error page
            }

            var alternativeMachine = _context.Machines
                .FirstOrDefault(m => m.AvailabilityStatus == "Available" && m.MachineId != machineId);

            if (alternativeMachine != null)
            {
                // Reassign to an alternative machine
                var conflict = new ConflictManager
                {
                    OriginalMachineId = machineId,
                    NewMachineId = alternativeMachine.MachineId,
                    ResolutionStatus = "Resolved"
                };

                _context.ConflictManagers.Add(conflict);
                alternativeMachine.AvailabilityStatus = "Booked";
                _context.SaveChanges();

                return RedirectToAction("BookingReassigned", new { newMachineId = alternativeMachine.MachineId });
            }
            else
            {
                // No available machines
                return View("NoMachinesAvailable");
            }
        }

        public ActionResult BookingReassigned(int newMachineId)
        {
            var machine = _context.Machines.Find(newMachineId);
            ViewBag.MachineName = machine?.MachineName; // Using the null-conditional operator
            return View();
        }
    }
}
