using FridgeProducts.Models;
using FridgeProducts.Repositories;
using FridgeProducts.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FridgeProducts.Controllers
{
    public class FridgeProductsController : Controller
    {
        private readonly IFridgeProductsService _fridgeProductsService;

        public FridgeProductsController(IFridgeProductsService fridgeProductsService)
        {
            _fridgeProductsService = fridgeProductsService;
        }

        [HttpGet]
        public IActionResult Add(Guid id)
        {
            ViewBag.Products = new SelectList(_fridgeProductsService.GetAllProducts(), "ProductId", "Name");
            return View();
        }

        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            var model = _fridgeProductsService.GetModel(id);
            if (model != null)
            {
                ViewBag.Products = new SelectList(_fridgeProductsService.GetAllProducts(), "ProductId", "Name", model.ProductId);
                return View(model);
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult Add(Models.FridgeProducts model)
        {
            if (ModelState.IsValid)
            {
                _fridgeProductsService.Create(model);
                return Redirect($"~/Fridge/About/{model.FridgeId}");
            }
            ViewBag.Products = new SelectList(_fridgeProductsService.GetAllProducts(), "ProductId", "Name");
            return View(model);
        }

        [HttpPost]
        public IActionResult Delete(Guid id, Guid FridgeId)
        {
            var model = _fridgeProductsService.GetModel(id);
            if (model != null)
            {
                _fridgeProductsService.Delete(model.Id);
                return Redirect($"~/Fridge/About/{FridgeId}");
            }

            return NotFound();
        }

        [HttpPost]
        public IActionResult Edit(Models.FridgeProducts model)
        {
            if (ModelState.IsValid)
            {
                _fridgeProductsService.Update(model);
                return Redirect($"~/Fridge/About/{model.FridgeId}");
            }

            return View(model);
        }
    }
}
