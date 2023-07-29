using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MyWebApiApp.Data;
using System.Linq;
using WebAPI.Data;
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


        
        public List<CartModel> Carts
        {
            get
            {
                var data = HttpContext.Session.Get<List<CartModel>>("GioHang");
                if (data == null)
                {
                    data = new List<CartModel>();
                }
                return data;
            }
        }

        /*
         var identity = HttpContext.User.Identity as ClaimsIdentity;
            var userId = identity.FindFirst("Id")?.Value;
            return Ok(userId); 
         */

        /*
         -login -> addToCart
         -login -> removeToCart
         -login -> order

        -khi user bam them gio hang -> mac dinh la them 1, khi nguoi dung bam vao cho do va them voi so luong
        -khi user vao trong trang gio hang va dieu chinh lai mac dinh so luong la bao nhieu 
         */


        [HttpPost("addToCart")]
        [Authorize]
        public IActionResult AddToCartUser(Guid id, int SoLuong)
        {
            var item = _context.Cart.Where(c => c.product_id == id).FirstOrDefault();            

            var myCart = Carts;
            

            if (item == null)
            {
                var hangHoa = _context.Products.SingleOrDefault(p => p.id == id);
                var _cartitem = new Cart
                {                    
                    product_id = id,
                    product_name = hangHoa.name,
                    price = hangHoa.price,
                    amount = SoLuong
                };
                _context.Add(_cartitem);
                _context.SaveChanges();                
            }
            else 
            {
                var item2 = _context.Cart.SingleOrDefault(c => c.cart_id == item.cart_id);
                item2.amount += SoLuong;
                _context.SaveChanges();
            }

            return Ok();
        }

        [HttpPost("updateQuantityCart")]
        [Authorize]
        public IActionResult updateCart(int id ,int SoLuong)
        {
            
                var item2 = _context.Cart.SingleOrDefault(c => c.cart_id == id);
                item2.amount = SoLuong;
                _context.SaveChanges();
            

            return Ok();
        }


        [HttpDelete]
        [Authorize]
        public IActionResult detete(int id)
        {

            var _cartitem = _context.Cart.SingleOrDefault(l => l.cart_id == id);
            if (_cartitem != null)
            {
                _context.Remove(_cartitem);
                _context.SaveChanges();
            }


            return Ok();
        }


        [HttpGet("user")]
        [Authorize]
        public IActionResult Index_User()
        {
            var cart = _context.Cart.Select(c => new CartModel
            {
                product_id = c.product_id,
                product_name = c.product_name,
                amount = c.amount,
                price = c.price,                
            });

            return Ok(cart);            
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
                item = new CartModel
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
