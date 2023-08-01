using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyWebApiApp.dataAccess;
using System.Data;
using System.Globalization;
using WebAPI.dtos;
using WebAPI.entities;
using WebAPI.repositories;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly MyDbContext _context;

        public ProductsController(IProductRepository productRepository, MyDbContext context)
        {
            _productRepository = productRepository;
            _context = context;
        }



        [HttpGet]        
        public IActionResult GetAllProducts(string? search, double? from, double? to, string? sortBy, int page = 1, int page_size = 10)
        {
            try
            {
                var result = _productRepository.GetAll(search, from, to, sortBy, page, page_size);
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            try
            {
                var result = _productRepository.GetById(id);
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }

        
        [HttpPost]
        [Authorize(Roles = "admin")]
        public IActionResult Add(ProductDto product)
        {
            try
            {
                _productRepository.Add(product);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut]
        [Authorize(Roles = "admin")]
        public IActionResult Update(ProductDto product)
        {
            try
            {
                _productRepository.Update(product);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                _productRepository.Delete(id);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }


        [HttpGet("categoryProduct/{id}")]
        public IActionResult classifyProductByCategory(int id)
        {
            try
            {
                var _products = _productRepository.classifyProductByCategory(id);
                return Ok(_products);
            }
            catch
            {
                return BadRequest();
            }
        }

    }
}
