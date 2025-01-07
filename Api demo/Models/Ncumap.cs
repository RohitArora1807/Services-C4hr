using System;
using System.Collections.Generic;

namespace Api_demo.Models;

public partial class Ncumap
{
    public int Uno { get; set; }

    public string Oid { get; set; } = null!;

    public string Lcode { get; set; } = null!;

    public string? Ulevel { get; set; }
}
