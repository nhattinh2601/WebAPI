using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyWebApiApp.Data;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly MyDbContext _context;

        public CartController(MyDbContext context)
        {
            _context = context;
        }

        
        public List<CartItem> Carts
        {
            get
            {
                var data = HttpContext.Session.Get<List<CartItem>>("GioHang");
                if (data == null)
                {
                    data = new List<CartItem>();
                }
                return data;
            }
        }

        [HttpGet]
        public IActionResult Index()
        {
            return Ok(Carts);
        }

        [HttpPost]
        public IActionResult AddToCart(Guid id, int SoLuong)
        {
            var myCart = Carts;
            var item = myCart.SingleOrDefault(p => p.product_id == id);

            if (item == null)//chưa có
            {
                var hangHoa = _context.Products.SingleOrDefault(p => p.id == id);
                item = new CartItem
                {
                    product_id = id,
                    product_name = hangHoa.name,
                    price = hangHoa.price,
                    amount = SoLuong,                    
                };
                myCart.Add(item);
            }
            else
            {
                item.amount += SoLuong;
            }
            HttpContext.Session.Set("GioHang", myCart);

            return Ok();
        }
    }
}
