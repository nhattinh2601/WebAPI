using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.entities
{
    [Table("Product")]
    public class Product
    {
        [Key]
        public Guid id { get; set; }

        [Required]
        [MaxLength(100)]
        public string name { get; set; }

        public string description { get; set; }

        [Range(0, double.MaxValue)]
        public double price { get; set; }

        public byte discount { get; set; }

        public int? category_id { get; set; }
        [ForeignKey("category_id")]
        public Category category { get; set; }

        public ICollection<OrderDetail> OrderDetails { set; get; }
        public Product()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

    }
}
