using WebAPI.dtos;
using WebAPI.entities;

namespace WebAPI.repositories
{
    public interface IProductRepository
    {
        List<ProductVM> GetAll(string search, double? from, double? to, string sortBy, int page = 1, int page_size=2);
        ProductVM GetById(Guid id);
        void Add(ProductDto product);
        void Delete(Guid id);
        void Update(ProductDto product);
        List<ProductVM> classifyProductByCategory(int id);
    }
}
