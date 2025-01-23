//namespace Api_demo.Services
//{
//    public interface IActService
//    {
//        string? GetLNameByStcodeAndAapl(string stcode, string aapl);
//    }
//}

using System.Collections.Generic;
using Api_demo.Models;

namespace Api_demo.Services
{
    public interface IActService
    {
        List<ActInfo> GetActsByTypeAndState(string type, string state);
    }
}
