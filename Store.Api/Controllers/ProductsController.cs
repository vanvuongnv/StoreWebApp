using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.Api.Data;
using Store.Api.Data.Entities;
using Store.Api.Models;

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

        [Route("add-product")]
        [HttpPost]
        public IActionResult AddProduct([FromBody] ProductDtoModel model)
        {
            if (ModelState.IsValid)
            {
                // valid category id
                var category = _context.Categories
                    .FirstOrDefault(x => x.CategoryId == model.CategoryId);

                if(category is null)
                {
                    return NotFound(new
                    {
                        state = false,
                        msg = "Category Not Found"
                    });
                }

                var entity = new Product()
                {
                    CategoryId = model.CategoryId,
                    Price = model.Price,
                    ProductName = model.ProductName?.Trim(),
                    UnitsInStock = model.UnitsInStock
                };
                _context.Products.Add(entity);
                try
                {
                    var result = _context.SaveChanges();
                    if(result > 0)
                    {
                        return Ok(entity);
                    }
                    return BadRequest(new
                    {
                        state = false,
                        msg = "Có lỗi trong quá trình lưu dữ liệu"
                    });
                }
                catch(Exception ex)
                {
                    return BadRequest(new
                    {
                        state = false,
                        msg = ex.Message
                    });
                }
            }
            return BadRequest(model);
        }

        [Route("update-product/{id}")]
        [HttpPost]
        public IActionResult UpdateProduct(int id, [FromBody] ProductDtoModel model)
        {
            if (ModelState.IsValid)
            {
                // valid product
                var product = _context.Products.FirstOrDefault(x => x.ProductId == id);
                if(product is null)
                {
                    return NotFound(model);
                }
                // valid category id
                var category = _context.Categories
                    .FirstOrDefault(x => x.CategoryId == model.CategoryId);

                if (category is null)
                {
                    return NotFound(new
                    {
                        state = false,
                        msg = "Category Not Found"
                    });
                }

                product.ProductName = model.ProductName?.Trim();
                product.Price = model.Price;
                product.CategoryId = model.CategoryId;
                product.UnitsInStock = model.UnitsInStock;
                
                try
                {
                    _context.Update(product);
                    var result = _context.SaveChanges();
                    if (result > 0)
                    {
                        return Ok(product);
                    }
                    return BadRequest(new
                    {
                        state = false,
                        msg = "Có lỗi trong quá trình lưu dữ liệu"
                    });
                }
                catch (Exception ex)
                {
                    return BadRequest(new
                    {
                        state = false,
                        msg = ex.Message
                    });
                }
            }
            return BadRequest(model);
        }

        [Route("delete-product/{id}")]
        [HttpDelete]
        public IActionResult DeleteProduct(int id)
        {
            if (ModelState.IsValid)
            {
                // valid product
                var product = _context.Products.FirstOrDefault(x => x.ProductId == id);
                if (product is null)
                {
                    return NotFound(id);
                }

                try
                {
                    _context.Remove(product);
                    var result = _context.SaveChanges();
                    if (result > 0)
                    {
                        return Ok(product);
                    }
                    return BadRequest(new
                    {
                        state = false,
                        msg = "Có lỗi trong quá trình lưu dữ liệu"
                    });
                }
                catch (Exception ex)
                {
                    return BadRequest(new
                    {
                        state = false,
                        msg = ex.Message
                    });
                }
            }
            return BadRequest(id);
        }
    }
}
