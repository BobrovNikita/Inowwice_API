using FridgeProducts.Models;

namespace FridgeProducts.Services.Interfaces
{
    public interface IFridgeModelsService
    {
        IEnumerable<FridgeModel> GetAll();
        FridgeModel GetModel(Guid id);
        void Create(FridgeModel item);
        void Delete(Guid id);
        void Update(FridgeModel item);
    }
}
