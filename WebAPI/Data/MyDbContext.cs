using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Data;

namespace MyWebApiApp.Data
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions options) : base(options) { }


        #region DbSet
        public DbSet<User> Users { get; set; }        
        public DbSet<Cart> Cart { get; set; }        
        
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        #endregion


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>(e =>
            {
                e.ToTable("Order");
                e.HasKey(dh => dh.order_id);
                e.Property(dh => dh.orderDate).HasDefaultValueSql("getutcdate()");
                e.Property(dh => dh.user).IsRequired().HasMaxLength(100);
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.ToTable("OrderDetail");
                entity.HasKey(e => new { e.product_id, e.order_id });

                entity.HasOne(e => e.Order)
                    .WithMany(e => e.OrderDetails)
                    .HasForeignKey(e => e.order_id)
                    .HasConstraintName("FK_OrderDetail_Order");

                entity.HasOne(e => e.Product)
                    .WithMany(e => e.OrderDetails)
                    .HasForeignKey(e => e.product_id)
                    .HasConstraintName("FK_OrderDetails_Product");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.username).IsUnique();
                entity.Property(e => e.fullname).IsRequired().HasMaxLength(150);
                entity.Property(e => e.email).IsRequired().HasMaxLength(150);

            });
        }
    }
}