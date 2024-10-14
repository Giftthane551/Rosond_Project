using System;
using System.Collections.Generic;

namespace Rosond_Project.Models;

public partial class Booking
{
    public int BookingId { get; set; }

    public int? CustomerId { get; set; }

    public int? MachineId { get; set; }

    public int? OperatorId { get; set; }

    public DateTime? BookingDate { get; set; }

    public string? Status { get; set; }

    public virtual ICollection<ConflictManager> ConflictManagers { get; set; } = new List<ConflictManager>();

    public virtual Customer? Customer { get; set; }

    public virtual Machine? Machine { get; set; }

    public virtual Operator? Operator { get; set; }
}
