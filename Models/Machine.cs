using System;
using System.Collections.Generic;

namespace Rosond_Project.Models;

public partial class Machine
{
    public int MachineId { get; set; }

    public string? MachineName { get; set; }

    public string? AvailabilityStatus { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual ICollection<ConflictManager> ConflictManagerNewMachines { get; set; } = new List<ConflictManager>();

    public virtual ICollection<ConflictManager> ConflictManagerOriginalMachines { get; set; } = new List<ConflictManager>();

    public virtual ICollection<Operator> Operators { get; set; } = new List<Operator>();
}
