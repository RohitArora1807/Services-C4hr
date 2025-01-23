using Api_demo.Models;

namespace Api_demo.Services
{
    public interface IRRDETService
    {
        double? GetNumericalF3(string stid);

        double? CLRAGetNumericalF1(string stid);
    }
}
