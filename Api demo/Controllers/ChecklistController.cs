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
        public IActionResult GenerateChecklistSubmission([FromBody] ChecklistRequest request)
        {
            var clientid = request.ClientId;
            var site = request.Site;
            var comtp = request.Comtp ?? string.Empty;

            var model = new GenerateActList
            {
                ClientList = _dbC4HRKBContext.ClientMasters
                    .Select(kb => new ClientList
                    {
                        ClientID = kb.ClientId.ToString(),
                        ClientName = kb.ClientName,
                        Active = kb.Active
                    }).ToList()
            };

            var siteList = _dbC4HRKBContext.SiteMasters
                .Where(s => s.ClientId == Convert.ToInt32(clientid))
                .ToList();

            var generateActs = new List<GenerateAct>();

            var filteredSites = site == "All"
                ? siteList
                : siteList.Where(s => s.SiteId == Convert.ToInt32(site)).ToList();
            // state and establishment type 
            //check list type list 
            foreach (var item in filteredSites)
            {
                var siteact = new GenerateAct
                {
                    SiteID = item.SiteId,
                    SiteName = item.SiteName?.Trim(),
                    State = item.State?.Trim(),
                    Type = item.Type?.Trim()
                };

                var applicableActs = _dbC4HRContext.Acts
                    .Where(act =>
                        act.Aactive == 1 &&
                        act.Alegtype == "A" &&
                        (act.Atype == "National" || act.Stcode == item.State) &&
                        ((item.Type == "F" && act.Appl != "E" && act.Appl != "S") ||
                         (item.Type == "E" && act.Appl != "F" && act.Appl != "S")) &&
                        _dbC4HRContext.Obligatis
                            .Where(o => string.IsNullOrEmpty(comtp) || o.Type == comtp)
                            .Select(o => o.Act)
                            .Distinct()
                            .Contains(act.Code)
                    )
                    .Select(act => new ActInfo
                    {
                        ActCode = act.Code.Trim(),
                        SActName = act.Sname.Trim(),
                        LActName = act.Lname.Trim(),
                        Year = act.Year.Trim(),
                        Appl = act.Appl.Trim()
                    }).ToList();

                siteact.Actlist = applicableActs;

                siteact.StateActlist = siteact.Actlist.SelectMany(act =>
                    _dbC4HRContext.Acts
                        .Where(s => s.Parleg == act.ActCode && s.Stcode == item.State)
                        .Select(s => new ActInfo
                        {
                            ActCode = s.Code.Trim(),
                            SActName = s.Sname.Trim(),
                            LActName = s.Lname.Trim(),
                            Year = s.Year.Trim(),
                            Appl = s.Appl.Trim()
                        })).ToList();

                generateActs.Add(siteact);
            }

            model.GenerateActs = generateActs;
            return Ok(model);
        }

    }


}
