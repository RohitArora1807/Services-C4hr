﻿using System;
using System.Collections.Generic;

namespace Api_demo.Models;

public partial class Nclocmap
{
    public string Oid { get; set; } = null!;

    public string Lcode { get; set; } = null!;

    public int Oblig { get; set; }

    public string? Acode { get; set; }
}
