using Microsoft.EntityFrameworkCore;
using WebAPI.entities;

namespace MyWebApiApp.dataAccess
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions options) : base(options) { }


        #region DbSet
        public DbSet<User> Users { get; set; }        
        public DbSet<Role> Roles { get; set; }        
        public DbSet<CartItem> CartItems { get; set; }                
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        #endregion
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
                    
        }

    }
}