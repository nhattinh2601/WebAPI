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
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        #endregion
    }
}