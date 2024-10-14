using System;
using System.Collections.Generic;

namespace Rosond_Project.Models;

public partial class Operator
{
    public int OperatorId { get; set; }

    public string? FullName { get; set; }

    public int? MachineId { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual Machine? Machine { get; set; }
}
