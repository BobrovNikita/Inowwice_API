using FridgeProducts.Models;
using FridgeProducts.Repositories;
using FridgeProducts.Services.Interfaces;

namespace FridgeProducts.Services
{
    public class FridgeService : IFridgeService
    {
        private readonly IRepository<Fridge> _fridgesRepository;
        private readonly IRepository<FridgeModel> _fridgeModelsRepository;
        private readonly IRepository<Product> _productsRepository;
        private readonly IRepository<Models.FridgeProducts> _fridgeProductsRepository;

        public FridgeService(IRepository<Fridge> fridgesRepository, IRepository<FridgeModel> fridgeModelsRepository, IRepository<Product> productsRepository, IRepository<Models.FridgeProducts> fridgeProductsRepository)
        {
            _fridgesRepository = fridgesRepository;
            _fridgeModelsRepository = fridgeModelsRepository;
            _productsRepository = productsRepository;
            _fridgeProductsRepository = fridgeProductsRepository;
        }

        public void Create(Fridge item, IFormFileCollection files, string webRootPath, Dictionary<string, int?> products)
        {
            string upload = webRootPath + FilePath.ImagePath;
            string fileName = Guid.NewGuid().ToString();

            string extension = Path.GetExtension(files[0].FileName);

            using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
            {
                files[0].CopyTo(fileStream);
            }

            item.Image = fileName + extension;

            _fridgesRepository.Create(item);

            foreach (var p in products)
            {
                if (p.Value != null)
                {
                    Models.FridgeProducts? fp = new()
                    {
                        FridgeId = item.FridgeId,
                        ProductId = Guid.Parse(p.Key),
                        Quantity = (int)p.Value
                    };
                    _fridgeProductsRepository.Create(fp);
                }
            }
            _fridgesRepository.Save();
        }

        public void Create(Models.FridgeProducts item)
        {
            _fridgeProductsRepository.Create(item);
            _fridgeProductsRepository.Save();
        }

        public void Delete(Guid id)
        {
            _fridgesRepository.Delete(id);
            _fridgesRepository.Save();
        }

        public IEnumerable<Fridge> GetAll() => _fridgesRepository.GetAll();

        public IEnumerable<FridgeModel> GetAllFridgeModels() => _fridgeModelsRepository.GetAll();

        public IEnumerable<Models.FridgeProducts> GetAllFridgeProducts(Guid id)
        {
            var models = _fridgeProductsRepository.GetAll();

            return models.Where(m => m.FridgeId == id).ToList();
        }

        public IEnumerable<Product> GetAllProducts() => _productsRepository.GetAll();

        public Fridge GetModel(Guid id) => _fridgesRepository.GetModel(id);

        public void Update(Fridge item, IFormFileCollection files, string webRootPath)
        {

            if (files.Count > 0)
            {
                string upload = webRootPath + FilePath.ImagePath;
                string fileName = Guid.NewGuid().ToString();
                string extension = Path.GetExtension(files[0].FileName);

                var oldFile = Path.Combine(upload, item.Image);

                if (File.Exists(oldFile))
                {
                    File.Delete(oldFile);
                }

                using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                {
                    files[0].CopyTo(fileStream);
                }

                item.Image = fileName + extension;
            }

            _fridgesRepository.Update(item);
            _fridgesRepository.Save();
        }
    }
}
