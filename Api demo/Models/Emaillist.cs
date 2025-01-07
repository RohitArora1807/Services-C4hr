using System;
using System.Collections.Generic;

namespace Api_demo.Models;

public partial class Emaillist
{
    public string Email { get; set; } = null!;

    public string Category { get; set; } = null!;

    public bool Active { get; set; }
}
