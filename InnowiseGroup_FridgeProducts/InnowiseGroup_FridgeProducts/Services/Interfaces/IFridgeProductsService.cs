using FridgeProducts.Models;
using FProducts = FridgeProducts.Models.FridgeProducts;

namespace FridgeProducts.Services.Interfaces
{
    public interface IFridgeProductsService
    {
        IEnumerable<Product> GetAllProducts();
        IEnumerable<FProducts> GetAll();
        FProducts GetModel(Guid id);
        void Create(FProducts item);
        void Delete(Guid id);
        void Update(FProducts item);
    }
}
