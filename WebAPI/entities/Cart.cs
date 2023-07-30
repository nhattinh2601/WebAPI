using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.entities
{
    [Table("Cart")]
    public class Cart
    {
        [Key]
        public int cart_id { get; set; }

        public Guid product_id { get; set; }

        [ForeignKey(nameof(product_id))]
        public Product Product { get; set; }

        public string product_name { get; set; }
        public double price { get; set; }
        public int amount { get; set; }
        public double total_cost => amount * price;

        public int user_id { get; set; }

        [ForeignKey(nameof(user_id))]
        public User User { get; set; }
    }
}
