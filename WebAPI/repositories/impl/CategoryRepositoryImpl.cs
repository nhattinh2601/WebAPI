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
                Name = loai.name
            };
            _context.Add(_category);
            _context.SaveChanges();

            return new CategoryVM
            {
                id = _category.Id,
                Name = _category.Name
            };
        }

        public void Delete(int id)
        {
            var _category = _context.Categories.SingleOrDefault(l => l.Id == id);
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
                id = lo.Id,
                Name = lo.Name
            });
            return loais.ToList();
        }

        public CategoryVM GetById(int id)
        {
            var category = _context.Categories.SingleOrDefault(l => l.Id == id);

            if (category != null)
            {
                return new CategoryVM
                {
                    id = category.Id,
                    Name = category.Name
                };
            }
            return null;
        }       

        public void Update(CategoryVM loai)
        {
            var _category = _context.Categories.SingleOrDefault(l => l.Id == loai.id);
            if (_category != null)
            {
                _category.Name = loai.Name;
                _context.SaveChanges();
            }
        }
    }
}
