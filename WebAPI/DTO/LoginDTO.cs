using System.ComponentModel.DataAnnotations;

namespace authentication_authorization_webapi.DTO
{
    public class LoginDTO
    {
        [Required]
        [MaxLength(50)]
        public string username { get; set; }
        [Required]
        [MaxLength(250)]
        public string password { get; set; }
    }
}
