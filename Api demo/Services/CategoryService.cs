using Api_demo.Models;

namespace Api_demo.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly DbC4HRContext _context;

        public CategoryService(DbC4HRContext context)
        {
            _context = context;
        }

        public IEnumerable<Mwcat> GetAllCategories()
        {
            return _context.Mwcats.ToList();
        }

        public IEnumerable<Mwcat> GetCategoriesByStid(string stid)
        {
            return _context.Mwcats.Where(c => c.Stid == stid).ToList();
        }

        public IEnumerable<Mwcat> GetCategoriesByStidAndGroup(string stid)  // fetching where Group is SE
        {
            return _context.Mwcats.Where(c => c.Stid == stid && c.Catgrp == "se").ToList();  
        }

        //public IEnumerable<Mwcat> GetCatIdGrpNameByStid(string stid)          //Fetching Catid, Catgrp, Catname -> using stid
        //{
        //    return _context.Mwcats
        //.Where(c => c.Stid == stid)
        //.Select(c => new
        //{
        //    c.Catid,
        //    c.Catgrp,
        //    c.Catname
        //})
        //.ToList();
        //}
    }
}

