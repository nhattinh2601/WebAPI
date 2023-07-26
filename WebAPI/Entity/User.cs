using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace authentication_authorization_webapi.Entity
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

        public int role_id { get; set; }

        [ForeignKey(nameof(role_id))]
        public  Role Role { get; set; }

        public string GetRoleName()
        {
            if (role_id == 1)
            {
                return "admin";
            }
            else if (role_id == 2)
            {
                return "user";
            }
            else
            {
                // Nếu role_id không phù hợp với các giá trị trên, trả về null hoặc giá trị mặc định khác tùy ý.
                return "user";
            }
        }
    }
}
