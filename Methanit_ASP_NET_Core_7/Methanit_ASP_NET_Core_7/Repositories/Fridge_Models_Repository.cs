using Methanit_ASP_NET_Core_7.Models;

namespace Methanit_ASP_NET_Core_7.Repositories
{
    public class Fridge_Models_Repository : IRepository<Fridge_Model>
    {
        ApplicationContext db;

        public Fridge_Models_Repository(ApplicationContext context)
        {
            db = context;
        }

        public IEnumerable<Fridge_Model> GetAll()
        {
            return db.Fridge_Models.ToList();
        }

        public Fridge_Model GetModel(Guid id)
        {
            return db.Fridge_Models.First(f => f.Fridge_ModelId == id);
        }

        public void Create(Fridge_Model item)
        {
            db.Fridge_Models.Add(item);
        }

        public void Delete(Guid id)
        {
            db.Fridge_Models.Remove(GetModel(id));
        }

        public void Update(Fridge_Model item)
        {
            db.Fridge_Models.Update(item);
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}
