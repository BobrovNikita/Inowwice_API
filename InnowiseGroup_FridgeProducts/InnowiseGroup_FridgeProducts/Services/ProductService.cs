using FridgeProducts.Models;
using FridgeProducts.Repositories;
using FridgeProducts.Services.Interfaces;

namespace FridgeProducts.Services
{
    public class ProductService : IProductsService
    {
        private readonly IRepository<Product> _productRepository;

        public ProductService(IRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }

        public void Create(Product item)
        {
            _productRepository.Create(item);
            _productRepository.Save();
        }

        public void Delete(Guid id)
        {
            _productRepository.Delete(id);
            _productRepository.Save();
        }

        public IEnumerable<Product> GetAll() => _productRepository.GetAll();

        public Product GetModel(Guid id) => _productRepository.GetModel(id);

        public void Update(Product item)
        {
            _productRepository.Update(item);
            _productRepository.Save();
        }
    }
}
