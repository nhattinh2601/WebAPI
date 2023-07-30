using WebAPI.dtos;
using WebAPI.repositories;

namespace WebAPI.repositories.impl
{
    public class CategoryRepositoryInMemoryImpl : ICategoryRepository
    {
        static List<CategoryVM> loais = new List<CategoryVM>
        {
            new CategoryVM{category_id = 1, name = "Tivi"},
            new CategoryVM{category_id = 2, name = "Tủ lạnh"},
            new CategoryVM{category_id = 3, name = "Điều hòa"},
            new CategoryVM{category_id = 4, name = "Máy giặt"},
        };

        public CategoryVM Add(CategoryModel loai)
        {
            var _loai = new CategoryVM
            {
                category_id = loais.Max(lo => lo.category_id) + 1,
                name = loai.name
            };
            loais.Add(_loai);
            return _loai;
        }

        public void Delete(int id)
        {
            var _loai = loais.SingleOrDefault(lo => lo.category_id == id);
            if (_loai != null)
            {
                loais.Remove(_loai);
            }
        }

        public List<CategoryVM> GetAll()
        {
            return loais;
        }

        public CategoryVM GetById(int id)
        {
            var loai = loais.SingleOrDefault(lo => lo.category_id == id);
            if (loai != null)
            {
                return loai;
            }
            return null;

        }

        public void Update(CategoryVM loai)
        {
            var _loai = loais.SingleOrDefault(lo => lo.category_id == loai.category_id);
            if (_loai != null)
            {
                _loai.name = loai.name;
            }

        }
    }
}
