


// Request DTO
public class ChecklistRequest
{
    public string ClientId { get; set; }
    public string Site { get; set; }
    public string Comtp { get; set; }
}


// Response Models (match your existing ones)
public class GenerateActList
{
    public List<ClientList> ClientList { get; set; }
    public List<GenerateAct> GenerateActs { get; set; }
}

public class ClientList
{
    public string ClientID { get; set; }
    public string ClientName { get; set; }
    public bool Active { get; set; }
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

public class ActInfo
{
    public string ActCode { get; set; }
    public string SActName { get; set; }
    public string LActName { get; set; }
    public string Year { get; set; }
    public string Appl { get; set; }
}
