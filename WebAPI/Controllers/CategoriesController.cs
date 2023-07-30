using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyWebApiApp.dataAccess;
using WebAPI.entities;
using WebAPI.dtos;

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
        

        #region GetAll
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var dsLoai = _context.Categories.ToList();
                return Ok(dsLoai);
            }
            catch
            {
                return BadRequest();
            }
        }
        #endregion


        #region GetById
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
        #endregion


        #region Detele
        [HttpDelete("{id}")]
        public IActionResult DeleteById(int id)
        {
            var loai = _context.Categories.SingleOrDefault(lo => lo.category_id == id);
            if (loai != null)
            {
                _context.Categories.Remove(loai);
                _context.SaveChanges();
                return StatusCode(StatusCodes.Status200OK);
            }
            else
            {
                return NotFound();
            }
        }
        #endregion
        

        #region Create
        [HttpPost]
        [Authorize]
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
        #endregion


        #region Update
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
        #endregion
    }
}
