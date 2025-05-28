public class ChecklistRequest
{
    public string ClientId { get; set; }
    public string Site { get; set; }
    public string ComplianceType { get; set; }
}

public class ChecklistResponse
{
    public bool Success { get; set; }
    public string Message { get; set; }
    public GenerateActList Data { get; set; }
    public List<SiteInfo> SiteList { get; set; }
    public string ComplianceType { get; set; }
    public string ClientId { get; set; }
    public string Site { get; set; }
}

public class SiteInfo
{
    public int SiteId { get; set; }
    public string SiteName { get; set; }
    public string State { get; set; }
    public string Type { get; set; }
    public int ClientId { get; set; }
    public bool Active { get; set; }
}


// Existing models (assuming these exist in your project)
public class GenerateActList
{
    public List<ClientList> ClientList { get; set; }
    public List<GenerateAct> GenerateActs { get; set; }
}

public class GenerateAct
{
    public int SiteID { get; set; }
    public string SiteName { get; set; }
    public string State { get; set; }
    public string Type { get; set; }
    public List<ActInfo> Actlist { get; set; }
    public List<ActInfo> StateActlist { get; set; }
}

public class ClientList
{
    public string ClientID { get; set; }
    public string ClientName { get; set; }
    public bool Active { get; set; }
}

public class ActInfo
{
    public string ActCode { get; set; }
    public string SActName { get; set; }
    public string LActName { get; set; }
    public string Year { get; set; }
    public string Appl { get; set; }
}