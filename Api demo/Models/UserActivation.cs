using System;
using System.Collections.Generic;

namespace Api_demo.Models;

public partial class UserActivation
{
    public int UserId { get; set; }

    public Guid ActivationCode { get; set; }
}
