using System;
using System.Collections.Generic;

namespace C4HR_KB_PROJECT_KPMG.ModelsKB;

public partial class SiteMaster
{
    public int SiteId { get; set; }

    public int ClientId { get; set; }

    public string SiteName { get; set; } = null!;

    public string State { get; set; } = null!;

    public string City { get; set; } = null!;

    public string Type { get; set; } = null!;

    public int EmployeeHeadCount { get; set; }

    public int CemployeeHeadCount { get; set; }

    public bool Active { get; set; }

    public virtual ClientMaster Client { get; set; } = null!;
}
