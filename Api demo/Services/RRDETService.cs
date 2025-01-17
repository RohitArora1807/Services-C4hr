using Api_demo.Models;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Api_demo.Services
{
    public class RRDETService : IRRDETService
    {
        private readonly Db69605C4hr2Context _context;

        public RRDETService(Db69605C4hr2Context context)
        {
            _context = context;
        }

        public double? GetNumericalF3(string stid)
        {
            // Fetch the record where stid matches and rrid is fixed to 1
            var record = _context.Rrdets
                .FirstOrDefault(r => r.Stid == stid && r.Rrid == 1);

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
    }
}
