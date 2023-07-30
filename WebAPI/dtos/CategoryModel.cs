using System.ComponentModel.DataAnnotations;

namespace WebAPI.dtos
{
    public class CategoryModel
    {
        [Required]
        [MaxLength(50)]
        public string name { get; set; }
    }
}
