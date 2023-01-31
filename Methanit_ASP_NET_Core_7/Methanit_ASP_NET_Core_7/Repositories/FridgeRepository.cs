using Methanit_ASP_NET_Core_7.Models;
using Microsoft.EntityFrameworkCore;

namespace Methanit_ASP_NET_Core_7.Repositories
{
    public class FridgeRepository : IRepository<Fridge>
    {
        ApplicationContext db;
        public FridgeRepository(ApplicationContext context)
        {
            db = context;
        }

        public IEnumerable<Fridge> GetAll()
        {
            return db.Fridges.Include(f => f.Fridge_Model).ToList();
        }

        public Fridge GetModel(Guid id)
        {
            return db.Fridges.Include(f => f.Fridge_Model).First(f => f.FridgeId == id);
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
