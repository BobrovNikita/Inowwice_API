using FridgeProducts.Models;
using Microsoft.EntityFrameworkCore;

namespace FridgeProducts.Repositories
{
    public class FridgeRepository : IRepository<Fridge>
    {
        private readonly ApplicationContext db;
        public FridgeRepository(ApplicationContext context)
        {
            db = context;
        }

        public IEnumerable<Fridge> GetAll()
        {
            return db.Fridges.Include(f => f.FridgeModel).ToList();
        }

        public Fridge GetModel(Guid id)
        {
            return db.Fridges.Include(f => f.FridgeModel).First(f => f.FridgeId == id);
        }

        public void Create(Fridge item)
        {
            db.Fridges.Add(item);
        }

        public void Update(Fridge item)
        {
            db.Fridges.Update(item);
        }

        public void Delete(Guid id)
        {
            db.Fridges.Remove(GetModel(id));
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}
