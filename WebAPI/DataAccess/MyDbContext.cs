using authentication_authorization_webapi.Entity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace authentication_authorization_webapi.DataAccess
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions options) : base(options) { }


        #region DbSet
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        #endregion
    }
}
