using FridgeProducts.Models;
using FridgeProducts.Repositories;
using FridgeProducts.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FridgeProducts.Controllers
{
    public class FridgeModelsController : Controller
    {
        private readonly IFridgeModelsService _fridgeModelService;

        public FridgeModelsController(IFridgeModelsService fridgeModelsService)
        {
            _fridgeModelService = fridgeModelsService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var fridgeModels = _fridgeModelService.GetAll();
            return View(fridgeModels);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            var model = _fridgeModelService.GetModel(id);
            if (model != null)
                return View(model);

            return NotFound();
        }

        public IActionResult Deletes(Guid id)
        {
            var model = _fridgeModelService.GetModel(id);
            return PartialView("Deletes", model);
        }

        [HttpPost]
        public IActionResult Edit(FridgeModel model)
        {
            if (ModelState.IsValid)
            {
                _fridgeModelService.Update(model);
                return RedirectToAction("Index");
            }
            return View(model);

        }

        [HttpPost]
        public IActionResult Create(FridgeModel model)
        {
            if (ModelState.IsValid)
            {
                _fridgeModelService.Create(model);
                return RedirectToAction("Index");
            }
            return View(model);

        }

        [HttpPost]
        public IActionResult Delete(FridgeModel model)
        {
            _fridgeModelService.Delete(model.FridgeModelId);
            return RedirectToAction("Index");
        }
    }
}