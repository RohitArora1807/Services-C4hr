using Api_demo.Models;
using System.Collections.Generic;

namespace Api_demo.Services
{
    public interface IMWDATAService
    {
        IEnumerable<object> GetMonthlyData(string stid, int catid, double monthly);
    }
}