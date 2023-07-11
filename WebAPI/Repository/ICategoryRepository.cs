using WebAPI.Models;

namespace WebAPI.Repository
{
    public interface ICategoryRepository
    {
        List<CategoryVM> GetAll();
        CategoryVM GetById(int id);
        CategoryVM Add(CategoryModel loai);
        void Update(CategoryVM loai);
        void Delete(int id);
    }
}
