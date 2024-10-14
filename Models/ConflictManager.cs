using System;
using System.Collections.Generic;

namespace Rosond_Project.Models;

public partial class ConflictManager
{
    public int ConflictId { get; set; }

    public int? OriginalMachineId { get; set; }

    public int? NewMachineId { get; set; }

    public int? BookingId { get; set; }

    public string? ResolutionStatus { get; set; }

    public virtual Booking? Booking { get; set; }

    public virtual Machine? NewMachine { get; set; }

    public virtual Machine? OriginalMachine { get; set; }
}
