using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class CategoryModel
    {
        [Required]
        [MaxLength(50)]
        public string name { get; set; }
    }
}
