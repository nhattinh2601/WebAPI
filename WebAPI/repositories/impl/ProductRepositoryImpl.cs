using Microsoft.EntityFrameworkCore;
using MyWebApiApp.dataAccess;
using WebAPI.helpers;
using WebAPI.dtos;
using WebAPI.entities;

namespace WebAPI.repositories.impl
{
    public class ProductRepositoryImpl : IProductRepository
    {

        private readonly MyDbContext _context;        


        public ProductRepositoryImpl(MyDbContext context)
        {
            _context = context;
        }

        public void Add(ProductDto product)
        {
            var _product = new Product
            {
                Id = Guid.NewGuid(),
                Name = product.Name,
                Price = product.Price,
                Discount = product.Discount,
                Description = product.Description,
                CategoryId = product.CategoryId
            };
            _context.Add(_product);
            _context.SaveChanges();            
        }

        public List<ProductVM> classifyProductByCategory(int id)
        {
            var allProducts = _context.Products.Include(hh => hh.Category).Where(p => p.CategoryId == id);            
            var result = allProducts.Select(p => new ProductVM
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Description = p.Description,
                Discount = p.Discount,
                CategoryName = p.Category.Name
            });

            return result.ToList();
        }

        public void Delete(Guid id)
        {
            var _product = _context.Products.SingleOrDefault(p => p.Id == id);
            if( _product != null )
            {
                _context.Products.Remove( _product );
                _context.SaveChanges();
            }
        }

        public List<ProductVM> GetAll(string search, double? from, double? to, string sortBy, int page = 1, int page_size = 2)
        {
            //var allProducts = _context.Products.AsQueryable();
            //lấy thông tin của khóa ngoại trong 1 bảng 
            var allProducts = _context.Products.Include(hh => hh.Category).AsQueryable();
            #region Filtering
            if (!string.IsNullOrEmpty(search))
            {
                allProducts = allProducts.Where(p => p.Name.Contains(search));
            }
            if (from.HasValue)
            {
                allProducts = allProducts.Where(hh => hh.Price >= from);
            }
            if (to.HasValue)
            {
                allProducts = allProducts.Where(hh => hh.Price <= to);
            }
            #endregion

            #region Sorting
            //Default sort by Name (TenHh)
            allProducts = allProducts.OrderBy(p => p.Name);

            if (!string.IsNullOrEmpty(sortBy))
            {
                switch (sortBy)
                {
                    case "name_desc": allProducts = allProducts.OrderByDescending(hh => hh.Name); break;
                    case "price_asc": allProducts = allProducts.OrderBy(hh => hh.Price); break;
                    case "price_desc": allProducts = allProducts.OrderByDescending(hh => hh.Price); break;
                }
            }
            #endregion

            

            /*#region Paging
            allProducts = allProducts.Skip((page - 1) * PAGE_SIZE).Take(PAGE_SIZE);
            #endregion

            var result = allProducts.Select(p => new ProductModel
            {
                id = p.Id,
                name = p.Name,
                price = p.Price,
                category_name = p.Category.Name
            });

            return result.ToList();*/


            var result = PaginatedList<entities.Product>.Create(allProducts, page, page_size);

            return result.Select(hh => new ProductVM
            {
                Id = hh.Id,
                Name = hh.Name,
                Price = hh.Price,
                Discount = hh.Discount,
                CategoryName = hh.Category?.Name,
            }).ToList();
        }

        public ProductVM GetById(Guid id)
        {
            var product = _context.Products.Include(p => p.Category).SingleOrDefault(p => p.Id == id);

            if (product != null)
            {
                return new ProductVM
                {
                    Id = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                    Discount = product.Discount,
                    Description = product.Description,
                    CategoryName = product.Category.Name
                };
            }
            return null;
        }

        public void Update(ProductDto product)
        {
            var _product = _context.Products.SingleOrDefault(p => p.Id == product.Id);
            if (_product != null)
            {
                _product.Name = product.Name;
                _product.Price = product.Price;
                _product.Discount = product.Discount;
                _product.Description = product.Description;
                _product.CategoryId = product.CategoryId;
            }
            _context.SaveChanges();            
        }
    }
}
