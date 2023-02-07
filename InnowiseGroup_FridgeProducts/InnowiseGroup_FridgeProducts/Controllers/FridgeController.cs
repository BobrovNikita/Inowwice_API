using FridgeProducts;
using FridgeProducts.Models;
using FridgeProducts.Repositories;
using FridgeProducts.Services;
using FridgeProducts.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FridgeProducts.Controllers
{
    public class FridgeController : Controller
    {
        private readonly IFridgeService _fridgeService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FridgeController(IFridgeService fridgeService, IWebHostEnvironment webHostEnvironment)
        {
            _fridgeService = fridgeService;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(_fridgeService.GetAll());
        }

        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            var model = _fridgeService.GetModel(id);
            if (model != null)
            {
                ViewBag.Fridges = new SelectList(_fridgeService.GetAllFridgeModels(), "FridgeModelId", "Name", model.FridgeModelId);
                return View(model);
            }
            return NotFound();
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Fridges = new SelectList(_fridgeService.GetAllFridgeModels(), "FridgeModelId", "Name");
            ViewBag.Products = _fridgeService.GetAllProducts();
            return View();
        }

        [HttpGet]
        public IActionResult About(Guid id)
        {
            ViewBag.FridgesInProducts = _fridgeService.GetAllFridgeProducts(id);
            var model = _fridgeService.GetModel(id);
            if (model != null)
            {
                return View(model);
            }

            return NotFound();
        }

        public IActionResult Deletes(Guid id)
        {
            var model = _fridgeService.GetModel(id);
            return PartialView("Deletes", model);

        }

        [HttpPost]
        public IActionResult Delete(Fridge model)
        {
            _fridgeService.Delete(model.FridgeId);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Edit(Fridge model)
        {
            var files = HttpContext.Request.Form.Files;
            string webRootPath = _webHostEnvironment.WebRootPath;
            if (ModelState.IsValid && files.Count >= 0)
            {
                _fridgeService.Update(model, files, webRootPath);
                return RedirectToAction("About", new { id = model.FridgeId });
            }

            ViewBag.Fridges = new SelectList(_fridgeService.GetAllFridgeModels(), "FridgeModelId", "Name", model.FridgeModelId);
            ViewBag.Products = _fridgeService.GetAllProducts();
            return View(model);
        }

        [HttpPost]
        public IActionResult Create(Fridge model, Dictionary<string, int?> products)
        {
            var files = HttpContext.Request.Form.Files;
            string webRootPath = _webHostEnvironment.WebRootPath;
            if (ModelState.IsValid && files.Count > 0)
            {
                _fridgeService.Create(model, files, webRootPath, products);
                return RedirectToAction("Index");
            }

            ViewBag.Fridges = new SelectList(_fridgeService.GetAllFridgeModels(), "FridgeModelId", "Name");
            ViewBag.Products = _fridgeService.GetAllProducts();
            return View(model);
        }
    }
}
