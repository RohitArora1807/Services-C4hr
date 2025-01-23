//using Api_demo.Models;

//namespace Api_demo.Services
//{
//    public class ActService : IActService
//    {
//        private readonly Db69605C4hr2Context _context;

//        // Constructor for dependency injection of Db69605C4hr2Context
//        public ActService(Db69605C4hr2Context context)
//        {
//            _context = context;
//        }

//        public string? GetLNameByStcodeAndAapl(string stcode, string aapl)
//        {
//            // Validate input lengths
//            if (string.IsNullOrWhiteSpace(stcode) || string.IsNullOrWhiteSpace(aapl))
//                throw new ArgumentException("STCODE and AAPL cannot be null or empty.");

//            if (stcode.Length > 10 || aapl.Length > 10)
//                throw new ArgumentException("STCODE and AAPL should not exceed 10 characters.");

//            // Query the database for the matching record
//            var act = _context.Acts.FirstOrDefault(a => a.Stcode == stcode && a.Appl == aapl);

//            // Return the LNAME if found, otherwise null
//            return act?.Lname;
//        }
//    }
//}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using Api_demo.Models;

namespace Api_demo.Services
{
    public class ActService : IActService
    {
        private readonly Db69605C4hr2Context _dbC4HRContext;

        public ActService(Db69605C4hr2Context dbC4HRContext)
        {
            _dbC4HRContext = dbC4HRContext;
        }

        public List<ActInfo> GetActsByTypeAndState(string type, string state)
        {
            if (string.IsNullOrWhiteSpace(type) || string.IsNullOrWhiteSpace(state))
                throw new ArgumentException("Type and State cannot be null or empty.");

            //type = type.Trim();
            //state = state.Trim();

            var centerActs = (from act in _dbC4HRContext.Acts
                              where act.Aactive == 1
                                    && act.Alegtype == "A"
                                    && (act.Atype == "National" || act.Stcode == state)
                                    && ((type == "F" || type == "f" && act.Appl != "E" && act.Appl != "S") ||
                                        (type == "E" || type == "e" && act.Appl != "F" && act.Appl != "S"))
                                    && (from obligation in _dbC4HRContext.Obligatis
                                        select obligation.Act).Distinct().Contains(act.Code)
                              select new ActInfo
                              {
                                  ActCode = act.Code.Trim(),
                                  SActName = act.Sname.Trim(),
                                  LActName = act.Lname.Trim(),
                                  Year = act.Year.Trim(),
                                  Appl = act.Appl.Trim()
                              }).ToList();

            return centerActs;

        }
    }
}
