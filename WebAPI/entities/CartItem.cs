using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.entities
{
    [Table("CartItem")]
    public class CartItem
    {
        [Key]        
        public int Id { get; set; }

        public Guid ProductId { get; set; }
        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; }

        public string ProductName { get; set; }
        public double Price { get; set; }
        public int Amount { get; set; }
        public double TotalCost => Amount * Price;

        public int UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }
    }
}
