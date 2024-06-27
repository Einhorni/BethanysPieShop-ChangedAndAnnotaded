using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace BethanysPieShop.Models
{
    //this class is the bridge between the code of the application and the acutal database
    //changed DbContext to IdentityDbContext so that DbContext knows about users and roles
    public class BethanysPieShopDbContext : IdentityDbContext //comes with EFCore: DbContext (used without Auth)
    {
        public  BethanysPieShopDbContext (DbContextOptions<BethanysPieShopDbContext> options): base(options) //constructor needs an instance of the BdContext options)
        { 
        }

        //manage model classes / entitys with DbSet -> lets create tables in the db, update db
        public DbSet<Category> Categories { get; set; }
        public DbSet<Pie> Pies { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
    }
}
