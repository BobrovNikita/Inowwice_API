using FridgeProducts.Models;
using Microsoft.EntityFrameworkCore;


namespace FridgeProducts
{
    public class ApplicationContext : DbContext
    {
        public DbSet<FridgeModel> FridgeModels { get; set; }
        public DbSet<Fridge> Fridges { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Models.FridgeProducts> FridgeProducts { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        { }
    }
}
