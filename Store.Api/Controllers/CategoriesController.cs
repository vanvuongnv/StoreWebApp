using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.Api.Data;
using Store.Api.Models;

namespace Store.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        // injection db context
        private readonly StoreContext _context;
        public CategoriesController(StoreContext context)
        {
            _context = context;
        }

        // APIs Areas
        [HttpGet]
        [Route("get-categories")]
        public IActionResult GetCategories()
        {
            // SELECT * FROM Categories
            var categories = _context.Categories.ToList();

            // return HTTP STATUS 200 with list categories
            return Ok(categories);
        }

        [HttpGet]
        [Route("get-category-by-id")]
        public IActionResult GetCategoryById(int id)
        {
            // SELECT * FROM Categories WHERE CategoryId = @id
            var category = _context.Categories
                .Where(x => x.CategoryId == id)
                .FirstOrDefault();

            if(category is null)
            {
                // HTTP Status 404
                return NotFound(new { msg = "KO_TIM_THAY" });
            }
            return Ok(category);
        }

        [HttpGet]
        [Route("get-by-id/{id}")]
        public IActionResult GetById(int id)
        {
            // SELECT * FROM Categories WHERE CategoryId = @id
            var category = _context.Categories
                .Where(x => x.CategoryId == id)
                .FirstOrDefault();

            if (category is null)
            {
                // HTTP Status 404
                return NotFound(new { msg = "KO_TIM_THAY" });
            }
            return Ok(category);
        }

        [HttpPost]
        [Route("add-category")]
        public IActionResult AddCategory([FromBody] CategoryDtoModel model)
        {
            // validate input

            return Ok();
        }

    }
}
