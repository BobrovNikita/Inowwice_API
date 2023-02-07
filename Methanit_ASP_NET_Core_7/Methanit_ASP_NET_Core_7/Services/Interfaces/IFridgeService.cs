using FridgeProducts.Models;
using FProducts = FridgeProducts.Models.FridgeProducts;

namespace FridgeProducts.Services.Interfaces
{
    public interface IFridgeService
    {
        IEnumerable<Fridge> GetAll();
        IEnumerable<Product> GetAllProducts();
        IEnumerable<FProducts> GetAllFridgeProducts(Guid id);
        IEnumerable<FridgeModel> GetAllFridgeModels();

        Fridge GetModel(Guid id);
        void Create(Fridge item, IFormFileCollection files, string webRootPath, Dictionary<string, int?> products);
        void Create(FProducts item);
        void Delete(Guid id);
        void Update(Fridge item, IFormFileCollection files, string webRootPath);
    }
}
