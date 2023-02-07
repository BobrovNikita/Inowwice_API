using FridgeProducts.Models;
using FridgeProducts.Repositories;
using FridgeProducts.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FridgeProducts.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductsService _productsService;
        public ProductsController(IProductsService productsService)
        {
            _productsService = productsService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var products = _productsService.GetAll();
            return View(products);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            var model = _productsService.GetModel(id);
            if (model != null)
                return View(model);

            return NotFound();
        }

        public IActionResult Deletes(Guid id)
        {
                var model = _productsService.GetModel(id);
                return PartialView("Deletes", model);
        }

        [HttpPost]
        public IActionResult Create(Product model)
        {
            if (ModelState.IsValid)
            {
                _productsService.Create(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(Product model)
        {
            if (ModelState.IsValid)
            {
                _productsService.Update(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult Delete(Product model)
        {
            _productsService.Delete(model.ProductId);
            return RedirectToAction("Index");
        }
    }
}
