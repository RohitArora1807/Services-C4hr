using System;
using System.Collections.Generic;

namespace Api_demo.Models;

public partial class Calendar
{
    public decimal Oblig { get; set; }

    public DateTime Obldate { get; set; }

    public DateTime? Warndate { get; set; }

    public string? Remarks { get; set; }
}
