using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace authentication_authorization_webapi.Entity
{
    [Table("RefreshToken")]
    public class RefreshToken
    {
        [Key]
        public Guid Id { get; set; }

        public int user_id { get; set; }

        [ForeignKey(nameof(user_id))]
        public User User { get; set; }

        public string Token { get; set; }

        // tuong ung voi access token nao do
        public string JwtId { get; set; }

        public bool IsUsed { get; set; }


        // da thu hoi?
        public bool IsRevoked { get; set; }

        // tao ra luc nao?
        public DateTime IssueAt { get; set; }

        // time het han
        public DateTime ExpiredAt { get; set; }

    }
}
