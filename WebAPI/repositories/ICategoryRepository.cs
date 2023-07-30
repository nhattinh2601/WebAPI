using WebAPI.dtos;

namespace WebAPI.repositories
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
