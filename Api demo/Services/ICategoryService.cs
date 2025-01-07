using Api_demo.Models;
using System.Collections.Generic;

namespace Api_demo.Services
{
    public interface ICategoryService
    {
        IEnumerable<Mwcat> GetAllCategories();
        IEnumerable<Mwcat> GetCategoriesByStid(string stid);
    }
}
