using FridgeProducts.Models;
using Microsoft.EntityFrameworkCore;

namespace FridgeProducts.Repositories
{
    public class FridgeProductsRepository : IRepository<Models.FridgeProducts>
    {
        private readonly ApplicationContext db;

        public FridgeProductsRepository(ApplicationContext context)
        {
            db = context;
        }

        public IEnumerable<Models.FridgeProducts> GetAll()
        {
            return db.FridgeProducts.Include(p => p.Products).Include(f => f.Fridges).ToList();
        }

        public Models.FridgeProducts GetModel(Guid id)
        {
            return db.FridgeProducts.Include(p => p.Products).Include(f => f.Fridges).First(f => f.Id == id);
        }

        public void Create(Models.FridgeProducts item)
        {
            db.FridgeProducts.Add(item);
        }

        public void Update(Models.FridgeProducts item)
        {
            db.FridgeProducts.Update(item);
        }

        public void Delete(Guid id)
        {
            db.FridgeProducts.Remove(GetModel(id));
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}
