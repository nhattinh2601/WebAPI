using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace authentication_authorization_webapi.Entity
{
    [Table("Role")]
    public class Role
    {
        [Key]
        public int Id { get; set; }
        public virtual ICollection<User> Users { get; set; }

        [Required]
        [MaxLength(50)]
        public string role_name { get; set; }        
    }
}
