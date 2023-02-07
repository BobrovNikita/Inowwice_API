using FridgeProducts.Models;

namespace FridgeProducts.Repositories
{
    public class ProductsRepository : IRepository<Product>
    {
        private readonly ApplicationContext db;

        public ProductsRepository(ApplicationContext context)
        {
            db = context;
        }

        public IEnumerable<Product> GetAll()
        {
            return db.Products.ToList();
        }

        public Product GetModel(Guid id)
        {
            return db.Products.First(f => f.ProductId == id);
        }
        public void Create(Product item)
        {
            db.Products.Add(item);
        }

        public void Delete(Guid id)
        {
            db.Products.Remove(GetModel(id));
        }


        public void Update(Product item)
        {
            db.Products.Update(item);
        }

        public void Save()
        {
            db.SaveChanges();
        }

    }
}
