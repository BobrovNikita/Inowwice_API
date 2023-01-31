using Methanit_ASP_NET_Core_7.Models;

namespace Methanit_ASP_NET_Core_7.Repositories
{
    public class ProductsRepository : IRepository<Products>
    {
        ApplicationContext db;

        public ProductsRepository(ApplicationContext context)
        {
            db = context;
        }

        public IEnumerable<Products> GetAll()
        {
            return db.Products.ToList();
        }

        public Products GetModel(Guid id)
        {
            return db.Products.First(f => f.ProductsId == id);
        }
        public void Create(Products item)
        {
            db.Products.Add(item);
        }

        public void Delete(Guid id)
        {
            db.Products.Remove(GetModel(id));
        }


        public void Update(Products item)
        {
            db.Products.Update(item);
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
