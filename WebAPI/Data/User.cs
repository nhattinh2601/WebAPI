using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Data
{
    [Table("User")]
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string username { get; set; }
        [Required]
        [MaxLength(250)]
        public string password { get; set; }

        public string fullname { get; set; }

        public string email { get; set; }



    }
}
