using Api_demo.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Api_demo.Services
{
    public class MWDATAService : IMWDATAService
    {
        private readonly DbC4HRContext _context;

        public MWDATAService(DbC4HRContext context)
        {
            _context = context;
        }

        public IEnumerable<object> GetMonthlyData(string stid, int catid, double monthly)
        {
            var latestDate = _context.Mwdata                                           // latest date 
                .Where(d => d.Stid == stid && d.Catid == catid)
                .OrderByDescending(d => d.Stdate)   
                .Select(d => d.Stdate)
                .FirstOrDefault();

            if (latestDate == default(DateOnly))                                 // bychance date me koi dikkat
            {
                return new List<object> { new { status = "error", message = "No data found for the given stid and catid." } };
            }
                    
            var filteredData = _context.Mwdata                                 //Fetching Monthly
                .Where(d => d.Stid == stid
                        && d.Catid == catid
                        && d.Stdate == latestDate) 
                .Select(d => new
                {
                    d.Monthly
                })
                .FirstOrDefault(); 

            // If no matching data found, return an error message
            if (filteredData == null)
            {
                return new List<object> { new { status = "error", message = "No matching monthly data found for the given criteria." } };
            }

            if (monthly < filteredData.Monthly)
            {
                return new List<object>
                {
                    new
                    {
                        status = "error",
                        message = "Input monthly value is less than the existing value in the database.",
                        Monthly = filteredData.Monthly
                    }
                };
            }

            // Show success if the input monthly is greater than or equal to the existing monthly value
            return new List<object>
            {
                new
                {
                    status = "success",
                    monthly = filteredData.Monthly
                }
            };

            //// Return success with the matching monthly value
            //return new List<object> { new { status = "success", monthly = filteredData.Monthly } };
        }

        // New method to get all data
        public IEnumerable<object> GetAllData()
        {
            return _context.Mwdata.Select(d => new
            {
                d.Stid,
                d.Catid,
                d.Stdate,
                d.Monthly
            }).ToList();
        }

    }
}
