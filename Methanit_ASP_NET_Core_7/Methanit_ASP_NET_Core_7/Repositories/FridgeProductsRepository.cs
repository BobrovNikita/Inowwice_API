using Methanit_ASP_NET_Core_7.Models;
using Microsoft.EntityFrameworkCore;

namespace Methanit_ASP_NET_Core_7.Repositories
{
    public class FridgeProductsRepository : IRepository<Fridge_Products>
    {
        ApplicationContext db;

        public FridgeProductsRepository(ApplicationContext context)
        {
            db = context;
        }

        public IEnumerable<Fridge_Products> GetAll()
        {
            return db.FridgeProducts.Include(p => p.Products).Include(f => f.Fridges).ToList();
        }

        public IEnumerable<Fridge_Products> GetAll(Guid id)
        {
            return db.FridgeProducts.Include(p => p.Products).Include(f => f.Fridges).Where(f => f.FridgeId == id).ToList();
        }

        public Fridge_Products GetModel(Guid id)
        {
            return db.FridgeProducts.Include(p => p.Products).Include(f => f.Fridges).First(f => f.Id == id);
        }

        public void Create(Fridge_Products item)
        {
            db.FridgeProducts.Add(item);
        }

        public void Update(Fridge_Products item)
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


        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}
