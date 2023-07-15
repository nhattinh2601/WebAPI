using Microsoft.EntityFrameworkCore;
using MyWebApiApp.Data;
using System.Globalization;
using WebAPI.Models;

namespace WebAPI.Repository
{
    public class ProductRepository : IProductRepository
    {

        private readonly MyDbContext _context;
        public static int PAGE_SIZE { get; set; } = 2;
        

        public ProductRepository(MyDbContext context) 
        {
            _context = context;
        }

        public List<ProductModel> GetAll(string search, double? from, double? to, string sortBy, int page=1)
        {
            //var allProducts = _context.Products.AsQueryable();
            //lấy thông tin của khóa ngoại trong 1 bảng 
            var allProducts = _context.Products.Include(hh => hh.category).AsQueryable();
            #region Filtering
            if (!string.IsNullOrEmpty(search))
            {
                allProducts = allProducts.Where(p => p.name.Contains(search));
            }
            if (from.HasValue)
            {
                allProducts = allProducts.Where(hh => hh.price >= from);
            }
            if (to.HasValue)
            {
                allProducts = allProducts.Where(hh => hh.price <= to);
            }
            #endregion

            #region Sorting
            //Default sort by Name (TenHh)
            allProducts = allProducts.OrderBy(p => p.name);

            if (!string.IsNullOrEmpty(sortBy))
            {
                switch (sortBy)
                {
                    case "name_desc": allProducts = allProducts.OrderByDescending(hh => hh.name); break;
                    case "price_asc": allProducts = allProducts.OrderBy(hh => hh.price); break;
                    case "price_desc": allProducts = allProducts.OrderByDescending(hh => hh.price); break;
                }
            }
            #endregion


            /*
            #region Paging
            allProducts = allProducts.Skip((page - 1) * PAGE_SIZE).Take(PAGE_SIZE);
            #endregion
            
            


            var result = allProducts.Select(p => new ProductModel
            {
                id = p.id,
                name = p.name,
                price = p.price,
                category_name = p.category.name
            });

            return result.ToList();
            */

            var result = PaginatedList<WebAPI.Data.Product>.Create(allProducts, page, PAGE_SIZE);

            return result.Select(hh => new ProductModel
            {
                id = hh.id,
                name = hh.name,
                price = hh.price,
                category_name = hh.category?.name
            }).ToList();
        }
    }
}
