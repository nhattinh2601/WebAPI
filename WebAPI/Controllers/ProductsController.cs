using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        public static List<Product> products = new List<Product>();

        [HttpGet]
        public IActionResult getAll()
        {
            return Ok(products);
        }

        [HttpPost]
        public IActionResult create(Product productDTO)
        {
            var product = new Product
            {
                id = Guid.NewGuid(),
                name = productDTO.name,
                price = productDTO.price
            };
            products.Add(product);
            return Ok(new { 
                Success = true,
                Data = product
            });
        }

        [HttpGet("{id}")]
        public IActionResult get(string id)
        {
            try
            {
                var product = products.SingleOrDefault( p => p.id == Guid.Parse(id));
                if (product == null)
                {
                    return NotFound();
                }
                return Ok(product);
            }catch 
            {
                return BadRequest();
            }

            
        }

        
        [HttpPut("{id}")]
        public IActionResult update(string id,Product productDTO)
        {
            try
            {
                var product = products.SingleOrDefault(p => p.id == Guid.Parse(id));
                if (product == null)
                {
                    return NotFound();
                }
                products.Add(product);
                return Ok(new
                {
                    Success = true
                });
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public IActionResult delete(string id)
        {
            try
            {
                var product = products.SingleOrDefault(p => p.id == Guid.Parse(id));
                if (product == null)
                {
                    return NotFound();
                }
                products.Remove(product);
                return Ok(new
                {
                    Success = true
                });
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
