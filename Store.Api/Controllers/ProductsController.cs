using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.Api.Data;

namespace Store.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly StoreContext _context;
        public ProductsController(StoreContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("get-products")]
        public IActionResult GetProducts(int page = 1, int pageSize = 10)
        {
            // Select * from Products
            // Order ProductId Desc
            // OFFSET (@page - 1) * @pageSize
            // ROWS FETCH NEXT @pageSize ROWS ONLY
            var products = _context.Products
                .OrderByDescending(x => x.ProductId)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            // Select count(ProductId) From Products
            var total = _context.Products
                .Select(x => x.ProductId)
                .Count();

            return Ok(new
            {
                items = products,
                total
            });
        }
    }
}
