using FridgeProducts.Models;

namespace FridgeProducts.Services.Interfaces
{
    public interface IProductsService
    {
        IEnumerable<Product> GetAll();
        Product GetModel(Guid id);
        void Create(Product item);
        void Delete(Guid id);
        void Update(Product item);
    }
}
