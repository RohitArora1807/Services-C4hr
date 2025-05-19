using Api_demo.Logging;
using Api_demo.Models;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Api_demo.Services
{
    public class RRDETService : IRRDETService
    {
        private readonly DbC4HRContext _context;
        private readonly LoggerService _logger;
        public RRDETService(DbC4HRContext context, LoggerService logger)
        {
            _context = context;
            _logger = logger;
        }

        public double? GetNumericalF3(string stid)
        {
            // Fetch the record where stid matches and rrid is fixed to 1
            var record = _context.Rrdets
                .FirstOrDefault(r => r.Stid == stid.Trim() && r.Rrid == 1);

            // If no record is found or F3 is null, return null
            if (record == null || string.IsNullOrEmpty(record.F3))
            {
                return null;
            }

            // Extract the first numerical value from the F3 string
            var match = Regex.Match(record.F3, @"\d+(\.\d+)?");
            if (match.Success)
            {
                // Parse and return the matched value as a double
                return double.Parse(match.Value);
            }

            // Return null if no numerical value is found in F3
            return null;
        }
        public double? CLRAGetNumericalF1(string stid)
        {
            // Fetch the record where stid matches and rrid is fixed to 10
            _logger.LogError($"{DateTime.Now:yyyy - MM - dd HH: mm:ss} function called stid:{stid}acascqcqecqw+++++++++++++++wefcwefwe");
            
            var record = _context.Rrdets
                .FirstOrDefault(r => r.Stid.Replace("\u00A0"," ").Trim() == stid.Trim() && r.Rrid == 10);
            //_logger.LogError($"{DateTime.Now:yyyy - MM - dd HH: mm:ss} record value: {record.F1}");
            // If no record is found or F1 is null, return null
            if (record == null || string.IsNullOrEmpty(record.F1))
            {
                return null;
            }

            // Extract the first numerical value from the F1 string
            var match = Regex.Match(record.F1, @"\d+(\.\d+)?");
            if (match.Success)
            {
                // Parse and return the matched value as a double
                return double.Parse(match.Value);
            }

            // Return null if no numerical value is found in F1
            return null;
        }
    }
}
