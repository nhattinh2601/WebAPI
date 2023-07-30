using WebAPI.dtos;

namespace WebAPI.repositories
{
    public interface IProductRepository
    {
        List<ProductModel> GetAll(string search, double? from, double? to,string sortBy, int page=1);                
    }
}
