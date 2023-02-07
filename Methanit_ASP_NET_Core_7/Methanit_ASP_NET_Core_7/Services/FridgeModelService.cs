using FridgeProducts.Models;
using FridgeProducts.Repositories;
using FridgeProducts.Services.Interfaces;

namespace FridgeProducts.Services
{
    public class FridgeModelService : IFridgeModelsService
    {
        private readonly IRepository<FridgeModel> _fridgeModelRepository;

        public FridgeModelService(IRepository<FridgeModel> fridgeModelRepository)
        {
            _fridgeModelRepository = fridgeModelRepository;
        }

        public void Create(FridgeModel item)
        {
            _fridgeModelRepository.Create(item);
            _fridgeModelRepository.Save();
        }

        public void Delete(Guid id)
        {
            _fridgeModelRepository.Delete(id);
            _fridgeModelRepository.Save();
        }

        public IEnumerable<FridgeModel> GetAll() => _fridgeModelRepository.GetAll();

        public FridgeModel GetModel(Guid id) => _fridgeModelRepository.GetModel(id);

        public void Update(FridgeModel item)
        {
            _fridgeModelRepository.Update(item);
            _fridgeModelRepository.Save();
        }
    }
}
