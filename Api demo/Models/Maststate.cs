using System;
using System.Collections.Generic;

namespace Api_demo.Models;

public partial class Maststate
{
    public string Stateid { get; set; } = null!;

    public string? Statedesc { get; set; }

    public string? Stactive { get; set; }
}
