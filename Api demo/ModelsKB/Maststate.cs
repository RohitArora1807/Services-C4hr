using System;
using System.Collections.Generic;

namespace C4HR_KB_PROJECT_KPMG.ModelsKB;

public partial class Maststate
{
    public string Stateid { get; set; } = null!;

    public string? Statedesc { get; set; }

    public string? Stactive { get; set; }
}
