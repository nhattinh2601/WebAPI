using MyWebApiApp.dataAccess;
using WebAPI.entities;
using WebAPI.dtos;
using WebAPI.repositories;

namespace WebAPI.repositories.impl
{
    public class CategoryRepositoryImpl : ICategoryRepository
    {
        private readonly MyDbContext _context;

        public CategoryRepositoryImpl(MyDbContext context)
        {
            _context = context;
        }

        public CategoryVM Add(CategoryModel loai)
        {
            var _category = new Category
            {
                name = loai.name
            };
            _context.Add(_category);
            _context.SaveChanges();

            return new CategoryVM
            {
                category_id = _category.category_id,
                name = _category.name
            };
        }

        public void Delete(int id)
        {
            var _category = _context.Categories.SingleOrDefault(l => l.category_id == id);
            if (_category != null)
            {
                _context.Remove(_category);
                _context.SaveChanges();
            }
        }

        public List<CategoryVM> GetAll()
        {
            var loais = _context.Categories.Select(lo => new CategoryVM
            {
                category_id = lo.category_id,
                name = lo.name
            });
            return loais.ToList();
        }

        public CategoryVM GetById(int id)
        {
            var category = _context.Categories.SingleOrDefault(l => l.category_id == id);

            if (category != null)
            {
                return new CategoryVM
                {
                    category_id = category.category_id,
                    name = category.name
                };
            }
            return null;
        }

        public void Update(CategoryVM loai)
        {
            var _category = _context.Categories.SingleOrDefault(l => l.category_id == loai.category_id);
            if (_category != null)
            {
                _category.name = loai.name;
                _context.SaveChanges();
            }
        }
    }
}
