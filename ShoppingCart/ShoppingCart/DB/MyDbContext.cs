using Microsoft.EntityFrameworkCore;
using ShoppingCart.Models;

namespace ShoppingCart.DB
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {

        }

        // DBSet represents a Database Table in our database
        public DbSet<Good> Goods { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<OrderProductSerial> OrderProductSerials { get; set; }
        public DbSet<OrderGoodQuantity> OrderGoodQuantities { get; set; }
        public DbSet<Order> Orders { get; set; }


        /// <summary>
        /// 让这个表具有复合主键
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderGoodQuantity>()
                .HasKey(oq => new { oq.OrderId, oq.GoodId });
        }


    }
}
