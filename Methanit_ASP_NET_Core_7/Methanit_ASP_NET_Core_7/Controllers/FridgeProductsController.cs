using Methanit_ASP_NET_Core_7.Models;
using Methanit_ASP_NET_Core_7.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Methanit_ASP_NET_Core_7.Controllers
{
    public class FridgeProductsController : Controller
    {
        private readonly IRepository<Fridge_Products> _repository;
        private readonly IRepository<Product> _products;

        public FridgeProductsController(IRepository<Fridge_Products> repository, IRepository<Product> products)
        {
            _repository = repository;
            _products = products;
        }

        [HttpGet]
        public IActionResult Add(Guid? id)
        {
            if (id != null)
            {
                ViewBag.Products = new SelectList(_products.GetAll(), "ProductsId", "Name");
                return View();
            }

            return NotFound();
        }

        [HttpGet]
        public IActionResult Edit(Guid? id)
        {
            if (id != null)
            {
                Fridge_Products? model = _repository.GetModel((Guid)id);
                if (model != null)
                {
                    ViewBag.Products = new SelectList(_products.GetAll(), "ProductsId", "Name", model.ProductId);
                    return View(model);
                }
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult Add(Fridge_Products model)
        {
            if (ModelState.IsValid)
            {
                model.FridgeId = model.Id;
                model.Id = Guid.Empty;
                _repository.Create(model);
                _repository.Save();
                return Redirect($"~/Fridge/About/{model.FridgeId}");
            }
            ViewBag.Products = new SelectList(_products.GetAll(), "ProductsId", "Name");
            return View(model);            
        }

        [HttpPost]
        public IActionResult Delete(Guid? id, Guid? FridgeId)
        {
            if (id != null)
            {
                Fridge_Products? model = _repository.GetModel((Guid)id);
                if (model != null)
                {
                    _repository.Delete(model.Id);
                    _repository.Save();
                    return Redirect($"~/Fridge/About/{FridgeId}");
                }
            }

            return NotFound();
        }

        [HttpPost]
        public IActionResult Edit(Fridge_Products? model)
        {
            if (model != null)
            {
                if (ModelState.IsValid)
                {
                    _repository.Update(model);
                    _repository.Save();
                    return Redirect($"~/Fridge/About/{model.FridgeId}");
                }
                return View(model);
            }
            return NotFound();
        }
    }
}
