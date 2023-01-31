using Microsoft.EntityFrameworkCore;
using Methanit_ASP_NET_Core_7.Models;
using Microsoft.Data.SqlClient;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;


namespace Methanit_ASP_NET_Core_7
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Fridge_Model> Fridge_Models { get; set; }
        public DbSet<Fridge> Fridges { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Fridge_Products> FridgeProducts { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        { }
    }
}
