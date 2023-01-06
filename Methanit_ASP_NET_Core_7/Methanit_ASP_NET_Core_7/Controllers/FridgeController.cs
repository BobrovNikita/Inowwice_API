using Methanit_ASP_NET_Core_7.Models;
using Methanit_ASP_NET_Core_7.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections;
using System.IO;

namespace Methanit_ASP_NET_Core_7.Controllers
{
    public class FridgeController : Controller
    {
        private IRepository<Fridge> _repository;
        private IRepository<Fridge_Model> _fridgeModels;
        private IRepository<Products> _products;
        private IRepository<Fridge_Products> _fridgeProducts;

        private readonly IWebHostEnvironment _webHostEnvironment;

        public FridgeController(IRepository<Fridge> repository, IRepository<Fridge_Model> fridgeModels, IRepository<Products> products, IRepository<Fridge_Products> fridgeProducts, IWebHostEnvironment webHostEnvironment)
        {
            _repository = repository;
            _fridgeModels = fridgeModels;
            _products = products;
            _fridgeProducts = fridgeProducts;

            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(_repository.GetAll());
        }
        [HttpGet]
        public IActionResult Edit(Guid? id)
        {
            if (id != null)
            {
                Fridge? model = _repository.GetModel((Guid)id);
                if (model != null)
                {
                    ViewBag.Fridges = new SelectList(_fridgeModels.GetAll(), "Fridge_ModelId", "Name", model.Fridge_ModelId);
                    return View(model);
                }
            }
            
            return NotFound();
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Fridges = new SelectList(_fridgeModels.GetAll(), "Fridge_ModelId", "Name");
            ViewBag.Products = _products.GetAll();
            return View();
        }

        [HttpGet]
        public IActionResult About(Guid? id)
        {
            if(id != null)
            {
                ViewBag.FridgesInProducts = _fridgeProducts.GetAll((Guid)id);
                Fridge? model = _repository.GetModel((Guid)id);
                if (model != null)
                {
                    return View(model);
                }
                
            }

            return NotFound();
            
        }

        public IActionResult Deletes(Guid? id)
        {
            if (id != null)
            {
                Fridge? model = _repository.GetModel((Guid)id);
                return PartialView("Deletes", model);
            }
            else
            {
                return NotFound();
            }
            
        }

        [HttpPost]
        public IActionResult Delete(Fridge model)
        {
            _repository.Delete(model.FridgeId);
            _repository.Save();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Edit(Fridge model)
        {
            var files = HttpContext.Request.Form.Files;
            string webRootPath = _webHostEnvironment.WebRootPath;
            if (ModelState.IsValid && files.Count >= 0)
            {
                Fridge? fridge = _repository.GetModel(model.FridgeId);

                if (fridge != null)
                {
                    if (files.Count > 0)
                    {
                        string upload = webRootPath + WC.ImagePath;
                        string fileName = Guid.NewGuid().ToString();
                        string extension = Path.GetExtension(files[0].FileName);

                        var oldFile = Path.Combine(upload, fridge.Image);

                        if (System.IO.File.Exists(oldFile))
                        {
                            System.IO.File.Delete(oldFile);
                        }

                        using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                        {
                            files[0].CopyTo(fileStream);
                        }

                        fridge.Image = fileName + extension;
                    }
                    else
                    {
                        fridge.Image = model.Image;
                    }
                    fridge.Name = model.Name;
                    fridge.Owner_Name = model.Owner_Name;
                    _repository.Update(model);

                }

                _repository.Save();

                return RedirectToAction("About", new { id = model.FridgeId });
            }
            else
            {
                ViewBag.Fridges = new SelectList(_fridgeModels.GetAll(), "Fridge_ModelId", "Name", model.Fridge_ModelId);
                ViewBag.Products = _products.GetAll();
                return View(model);
            }
        }

        [HttpPost]
        public IActionResult Create(Fridge model, Dictionary<Guid, int?> products)
        {
                var files = HttpContext.Request.Form.Files;
                string webRootPath = _webHostEnvironment.WebRootPath;
            if (ModelState.IsValid && files.Count > 0)
            {
                string upload = webRootPath + WC.ImagePath;
                string fileName = Guid.NewGuid().ToString();
            
                string extension = Path.GetExtension(files[0].FileName);

                using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                {
                    files[0].CopyTo(fileStream);
                }

                model.Image = fileName + extension;

                _repository.Create(model);

                foreach (var item in products)
                {
                    if (item.Value != null)
                    {
                        Fridge_Products? fp = new Fridge_Products();
                        fp.FridgeId = model.FridgeId;
                        fp.ProductsId = item.Key;
                        fp.Quantity = (int)item.Value;
                        _fridgeProducts.Create(fp);
                    }
                }

                _repository.Save();
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Fridges = new SelectList(_fridgeModels.GetAll(), "Fridge_ModelId", "Name");
                ViewBag.Products = _products.GetAll();
                return View(model);
            }
            

        }
    }
}
