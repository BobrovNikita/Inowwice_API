using FridgeProducts.Models;

namespace FridgeProducts.Repositories
{
    public class FridgeModelsRepository : IRepository<FridgeModel>
    {
        private readonly ApplicationContext db;

        public FridgeModelsRepository(ApplicationContext context)
        {
            db = context;
        }

        public IEnumerable<FridgeModel> GetAll()
        {
            return db.FridgeModels.ToList();
        }

        public FridgeModel GetModel(Guid id)
        {
            return db.FridgeModels.First(f => f.FridgeModelId == id);
        }

        public void Create(FridgeModel item)
        {
            db.FridgeModels.Add(item);
        }

        public void Delete(Guid id)
        {
            db.FridgeModels.Remove(GetModel(id));
        }

        public void Update(FridgeModel item)
        {
            db.FridgeModels.Update(item);
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}
