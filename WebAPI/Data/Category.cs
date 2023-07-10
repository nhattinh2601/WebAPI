using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Data
{
    [Table("Category")]
    public class Category
    {
        [Key]
        public int category_id { get; set; }
        [Required]
        [MaxLength(50)]
        public string name { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
