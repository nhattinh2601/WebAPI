using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.entities
{
    [Table("Product")]
    public class Product
    {
        [Key]
        public Guid Id { get; set; }        

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public string Description { get; set; }

        [Range(0, double.MaxValue)]
        public double Price { get; set; }

        public byte Discount { get; set; }

        public int? CategoryId { get; set; }
        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; }

        public Product()
        {
            // Khởi tạo Category trong hàm khởi tạo
            Category = new Category();
        }
        public string GetCategoryName()
        {
            return Category.Name;
        }
    }
}
