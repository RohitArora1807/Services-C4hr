using System;
using System.Collections.Generic;

namespace C4HR_KB_PROJECT_KPMG.ModelsKB;

public partial class ClientMaster
{
    public int ClientId { get; set; }

    public string Hoaddress { get; set; } = null!;

    public string ContactName { get; set; } = null!;

    public string ContactEmail { get; set; } = null!;

    public string Natofbusiness { get; set; } = null!;

    public bool Active { get; set; }

    public string ClientName { get; set; } = null!;

    public virtual ICollection<SiteMaster> SiteMasters { get; set; } = new List<SiteMaster>();
}
