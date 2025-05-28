using Api_demo.Models;
using C4HR_KB_PROJECT_KPMG.ModelsKB;
using Microsoft.AspNetCore.Mvc;

namespace Api_demo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChecklistController : ControllerBase
    {
        private readonly DbC4HRKBContext _dbC4HRKBContext;
        private readonly DbC4HRContext _dbC4HRContext;

        public ChecklistController(DbC4HRKBContext dbC4HRKBContext, DbC4HRContext dbC4HRContext)
        {
            _dbC4HRKBContext = dbC4HRKBContext;
            _dbC4HRContext = dbC4HRContext;
        }

        [HttpPost("generate")]
        //  [HttpPost("GenerateChecklist")]

        
        [HttpPost("generate-checklist")]
        public IActionResult GenerateChecklistSubmission([FromBody] ChecklistRequest request)
        {
            try
            {
                var clientid = request.ClientId;
                var site = request.Site;
                var comtp = request.ComplianceType?.ToString() ?? "";

                Console.Write(site + "ye hai" + clientid);

                GenerateActList model = new GenerateActList();
                List<GenerateAct> generateActs = new List<GenerateAct>();

                // Get client list
                model.ClientList = (from kb in _dbC4HRKBContext.ClientMasters
                                    select new ClientList
                                    {
                                        ClientID = kb.ClientId.ToString(),
                                        ClientName = kb.ClientName,
                                        Active = kb.Active
                                    }).ToList();

                // Get site masters for the client
                var sm = (from kb in _dbC4HRKBContext.SiteMasters
                          where kb.ClientId == Convert.ToInt32(clientid)
                          select kb).ToList();

                // Convert to SiteInfo for response
                var siteInfoList = sm.Select(s => new SiteInfo
                {
                    SiteId = s.SiteId,
                    SiteName = s.SiteName,
                    State = s.State,
                    Type = s.Type,
                    ClientId = s.ClientId,
                    Active = s.Active
                }).ToList();

                if (site == "All")
                {
                    foreach (var item in sm)
                    {
                        GenerateAct siteact = new GenerateAct();
                        siteact.SiteID = item.SiteId;
                        siteact.SiteName = item.SiteName.Trim();
                        siteact.State = item.State.Trim();
                        siteact.Type = item.Type.Trim();

                        if (string.IsNullOrEmpty(comtp))
                        {
                            siteact.Actlist = GetActsForSiteType(item, null);
                        }
                        else
                        {
                            siteact.Actlist = GetActsForSiteType(item, comtp);
                        }

                        // Get state acts
                        siteact.StateActlist = GetStateActs(siteact.Actlist, item.State);
                        generateActs.Add(siteact);
                    }
                    model.GenerateActs = generateActs;
                }
                else
                {
                    GenerateAct siteact = new GenerateAct();
                    siteact.SiteID = Convert.ToInt32(site);
                    var sites = (from kb in _dbC4HRKBContext.SiteMasters
                                 where kb.SiteId == siteact.SiteID
                                 select kb).FirstOrDefault();

                    if (sites != null)
                    {
                        siteact.SiteName = sites.SiteName.Trim();
                        siteact.State = sites.State.Trim();
                        siteact.Type = sites.Type.Trim();

                        if (string.IsNullOrEmpty(comtp))
                        {
                            siteact.Actlist = GetActsForSiteType(sites, null);
                        }
                        else
                        {
                            siteact.Actlist = GetActsForSiteType(sites, comtp);
                        }

                        // Get state acts
                        siteact.StateActlist = GetStateActs(siteact.Actlist, sites.State);
                        generateActs.Add(siteact);
                    }

                    model.GenerateActs = generateActs;
                }

                // Return API response
                var response = new ChecklistResponse
                {
                    Success = true,
                    Data = model,
                    SiteList = siteInfoList,
                    ComplianceType = comtp,
                    ClientId = clientid,
                    Site = site
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new ChecklistResponse
                {
                    Success = false,
                    Message = ex.Message
                });
            }
        }

        private List<ActInfo> GetActsForSiteType(dynamic siteItem, string complianceType)
        {
            var siteType = siteItem.Type.Trim();
            var state = siteItem.State.Trim();

            if (siteType == "F")
            {
                return GetFactoryActs(state, complianceType);
            }
            else if (siteType == "E")
            {
                return GetEstablishmentActs(state, complianceType);
            }
            else if (siteType == "BO")
            {
                return new List<ActInfo>();
            }

            return new List<ActInfo>();
        }

        private List<ActInfo> GetFactoryActs(string state, string complianceType)
        {
            if (string.IsNullOrEmpty(complianceType))
            {
                return (from act in _dbC4HRContext.Acts
                        where act.Aactive == 1 && act.Alegtype == "A" &&
                              (act.Atype == "National" || act.Stcode == state) &&
                              act.Appl != "E" && act.Appl != "S" &&
                              (from obligation in _dbC4HRContext.Obligatis
                               select obligation.Act).Distinct().Contains(act.Code)
                        select new ActInfo
                        {
                            ActCode = act.Code.Trim(),
                            SActName = act.Sname.Trim(),
                            LActName = act.Lname.Trim(),
                            Year = act.Year.Trim(),
                            Appl = act.Appl.Trim()
                        }).ToList();
            }
            else
            {
                return (from act in _dbC4HRContext.Acts
                        where act.Aactive == 1 && act.Alegtype == "A" &&
                              (act.Atype == "National" || act.Stcode == state) &&
                              act.Appl != "E" && act.Appl != "S" &&
                              (from obligation in _dbC4HRContext.Obligatis
                               where obligation.Type.Trim() == complianceType
                               select obligation.Act).Distinct().Contains(act.Code)
                        select new ActInfo
                        {
                            ActCode = act.Code.Trim(),
                            SActName = act.Sname.Trim(),
                            LActName = act.Lname.Trim(),
                            Year = act.Year.Trim(),
                            Appl = act.Appl.Trim()
                        }).ToList();
            }
        }

        private List<ActInfo> GetEstablishmentActs(string state, string complianceType)
        {
            if (string.IsNullOrEmpty(complianceType))
            {
                return (from act in _dbC4HRContext.Acts
                        where act.Aactive == 1 && act.Alegtype == "A" &&
                              (act.Atype == "National" || act.Stcode == state) &&
                              act.Appl != "F" && act.Appl != "S" &&
                              (from obligation in _dbC4HRContext.Obligatis
                               select obligation.Act).Distinct().Contains(act.Code)
                        select new ActInfo
                        {
                            ActCode = act.Code.Trim(),
                            SActName = act.Sname.Trim(),
                            LActName = act.Lname.Trim(),
                            Year = act.Year.Trim(),
                            Appl = act.Appl.Trim()
                        }).ToList();
            }
            else
            {
                return (from act in _dbC4HRContext.Acts
                        where act.Aactive == 1 && act.Alegtype == "A" &&
                              (act.Atype == "National" || act.Stcode == state) &&
                              act.Appl != "F" && act.Appl != "S" &&
                              (from obligation in _dbC4HRContext.Obligatis
                               where obligation.Type.Trim() == complianceType
                               select obligation.Act).Distinct().Contains(act.Code)
                        select new ActInfo
                        {
                            ActCode = act.Code.Trim(),
                            SActName = act.Sname.Trim(),
                            LActName = act.Lname.Trim(),
                            Year = act.Year.Trim(),
                            Appl = act.Appl.Trim()
                        }).ToList();
            }
        }

        private List<ActInfo> GetStateActs(List<ActInfo> actList, string state)
        {
            List<ActInfo> stateacts = new List<ActInfo>();

            foreach (var act in actList)
            {
                var stateActList = (from sact in _dbC4HRContext.Acts
                                    where sact.Parleg.Trim() == act.ActCode.Trim() &&
                                          sact.Stcode.Trim() == state
                                    select new ActInfo
                                    {
                                        ActCode = sact.Code.Trim(),
                                        SActName = sact.Sname.Trim(),
                                        LActName = sact.Lname.Trim(),
                                        Year = sact.Year.Trim(),
                                        Appl = sact.Appl.Trim()
                                    }).ToList();

                if (stateActList.Count > 0)
                {
                    stateacts.AddRange(stateActList); // Fixed: was using Concat which doesn't modify the list
                }
            }

            return stateacts;
        }
    }



    //public IActionResult GenerateChecklistSubmission([FromBody] ChecklistRequest request)
    //{
    //    var clientid = request.ClientId;
    //    var site = request.Site;
    //    var comtp = request.Comtp ?? string.Empty;

    //    var model = new GenerateActList
    //    {
    //        ClientList = _dbC4HRKBContext.ClientMasters
    //            .Select(kb => new ClientList
    //            {
    //                ClientID = kb.ClientId.ToString(),
    //                ClientName = kb.ClientName,
    //                Active = kb.Active
    //            }).ToList()
    //    };

    //    var siteList = _dbC4HRKBContext.SiteMasters
    //        .Where(s => s.ClientId == Convert.ToInt32(clientid))
    //        .ToList();

    //    var generateActs = new List<GenerateAct>();

    //    var filteredSites = site == "All"
    //        ? siteList
    //        : siteList.Where(s => s.SiteId == Convert.ToInt32(site)).ToList();
    //    // state and establishment type 
    //    //check list type list 
    //    foreach (var item in filteredSites)
    //    {
    //        var siteact = new GenerateAct
    //        {
    //            SiteID = item.SiteId,
    //            SiteName = item.SiteName?.Trim(),
    //            State = item.State?.Trim(),
    //            Type = item.Type?.Trim()
    //        };

    //        var applicableActs = _dbC4HRContext.Acts
    //            .Where(act =>
    //                act.Aactive == 1 &&
    //                act.Alegtype == "A" &&
    //                (act.Atype == "National" || act.Stcode == item.State) &&
    //                ((item.Type == "F" && act.Appl != "E" && act.Appl != "S") ||
    //                 (item.Type == "E" && act.Appl != "F" && act.Appl != "S")) &&
    //                _dbC4HRContext.Obligatis
    //                    .Where(o => string.IsNullOrEmpty(comtp) || o.Type == comtp)
    //                    .Select(o => o.Act)
    //                    .Distinct()
    //                    .Contains(act.Code)
    //            )
    //            .Select(act => new ActInfo
    //            {
    //                ActCode = act.Code.Trim(),
    //                SActName = act.Sname.Trim(),
    //                LActName = act.Lname.Trim(),
    //                Year = act.Year.Trim(),
    //                Appl = act.Appl.Trim()
    //            }).ToList();

    //        siteact.Actlist = applicableActs;

    //        siteact.StateActlist = siteact.Actlist.SelectMany(act =>
    //            _dbC4HRContext.Acts
    //                .Where(s => s.Parleg == act.ActCode && s.Stcode == item.State)
    //                .Select(s => new ActInfo
    //                {
    //                    ActCode = s.Code.Trim(),
    //                    SActName = s.Sname.Trim(),
    //                    LActName = s.Lname.Trim(),
    //                    Year = s.Year.Trim(),
    //                    Appl = s.Appl.Trim()
    //                })).ToList();

    //        generateActs.Add(siteact);
    //    }

    //    model.GenerateActs = generateActs;
    //    return Ok(model);
    //}

}



