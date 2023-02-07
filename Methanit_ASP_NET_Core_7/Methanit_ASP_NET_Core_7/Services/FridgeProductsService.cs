using FridgeProducts.Models;
using FridgeProducts.Repositories;
using FridgeProducts.Services.Interfaces;
using FProducts = FridgeProducts.Models.FridgeProducts;

namespace FridgeProducts.Services
{
    public class FridgeProductsService : IFridgeProductsService
    {
        private readonly IRepository<FProducts> _fridgeProductsRepository;
        private readonly IRepository<Product> _productRepository;

        public FridgeProductsService(IRepository<FProducts> fridgeProductsRepository, IRepository<Product> productRepository)
        {
            _fridgeProductsRepository = fridgeProductsRepository;
            _productRepository = productRepository;
        }

        public void Create(Models.FridgeProducts item)
        {
            item.FridgeId = item.Id;
            item.Id = Guid.Empty;
            _fridgeProductsRepository.Create(item);
            _fridgeProductsRepository.Save();
        }

        public void Delete(Guid id)
        {
            _fridgeProductsRepository.Delete(id);
            _fridgeProductsRepository.Save();
        }

        public IEnumerable<Models.FridgeProducts> GetAll() => _fridgeProductsRepository.GetAll();

        public IEnumerable<Product> GetAllProducts() => _productRepository.GetAll();

        public Models.FridgeProducts GetModel(Guid id) => _fridgeProductsRepository.GetModel(id);

        public void Update(Models.FridgeProducts item)
        {
            _fridgeProductsRepository.Update(item);
            _fridgeProductsRepository.Save();
        }
    }
}
