using BookLibrary.DAL.Entities;
using BookLibrary.DAL.Entities.OrderModel;
using Microsoft.EntityFrameworkCore;

namespace BookLibrary.DAL.DatabaseSecret
{
    public class StoreContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> Roles { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }


        public StoreContext(DbContextOptions<StoreContext> options) : base(options)
        {
            //Database.EnsureCreated();
        }
    }
}
