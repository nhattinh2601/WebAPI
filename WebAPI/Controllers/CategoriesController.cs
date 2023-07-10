using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyWebApiApp.Data;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly MyDbContext _context;

        public CategoriesController(MyDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var dsLoai = _context.Categories.ToList();
            return Ok(dsLoai);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var loai = _context.Categories.SingleOrDefault(lo => lo.category_id == id);
            if (loai != null)
            {
                return Ok(loai);
            }
            else
            {
                return NotFound();
            }
        }


        [HttpPost]
        public IActionResult CreateNew(CategoryModel model)
        {
            try
            {
                var loai = new Category
                {
                    name = model.name
                };
                _context.Add(loai);
                _context.SaveChanges();
                return Ok(loai);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateLoaiById(int id, CategoryModel model)
        {
            var loai = _context.Categories.SingleOrDefault(lo => lo.category_id == id);
            if (loai != null)
            {
                loai.name = model.name;
                _context.SaveChanges();
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }
    }
}
