using System.ComponentModel.DataAnnotations;

namespace WebAPI.dtos
{
    public class LoginModel
    {
        [Required]
        [MaxLength(50)]
        public string username { get; set; } 
        [Required]
        [MaxLength(250)]
        public string password { get; set; } 
    }
}
