using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;
using WebAPI.Repository;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var categories = _categoryRepository.GetAll();
                return Ok(categories);
            }
            catch 
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var category = _categoryRepository.GetById(id);
                if(category != null)
                {
                    return Ok(category);
                }else { return BadRequest(); }
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }



        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _categoryRepository.Delete(id);
                return Ok();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        
        [HttpPost]
        public IActionResult Add(CategoryModel category)
        {
            try
            {
                return Ok(_categoryRepository.Add(category));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id,CategoryVM loai)
        {
            if (id != loai.category_id)
            {
                return BadRequest();
            }
            try
            {
                _categoryRepository.Update(loai);
                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        
    }
}
