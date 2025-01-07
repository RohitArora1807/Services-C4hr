using Api_demo.Models;
using Api_demo.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Api_demo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // GET: api/category
        [HttpGet]
        public IActionResult GetCategories()
        {
            var categories = _categoryService.GetAllCategories();
            return Ok(categories);
        }

        [HttpPost("GetByStid")]
        public IActionResult GetCategoriesByStid(string stid)
        {
            var categories = _categoryService.GetCategoriesByStid(stid);

            if (categories == null || !categories.Any())
            {
                return NotFound(new { Message = "No categories found for the provided STID." });
            }

            return Ok(categories);
        }
    }
}
