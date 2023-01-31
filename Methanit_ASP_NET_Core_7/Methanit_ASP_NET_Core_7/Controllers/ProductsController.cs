using Methanit_ASP_NET_Core_7.Models;
using Methanit_ASP_NET_Core_7.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Methanit_ASP_NET_Core_7.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IRepository<Product> _repository;
        public ProductsController(IRepository<Product> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(_repository.GetAll());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Edit(Guid? id)
        {
            if (id != null)
            {
                Product? model = _repository.GetModel((Guid)id);
                if (model != null)
                    return View(model);
            }
            return NotFound();
        }

        public IActionResult Deletes(Guid? id)
        {
            if (id != null)
            {
                Product? model = _repository.GetModel((Guid)id);
                return PartialView("Deletes", model);
            }
            return NotFound();
        }
        
        [HttpPost]
        public IActionResult Create(Product model)
        {
            if (ModelState.IsValid)
            {
                _repository.Create(model);
                _repository.Save();
                return RedirectToAction("Index");
            }
            return View(model);
            
        }

        [HttpPost]
        public IActionResult Edit(Product model)
        {
            if (ModelState.IsValid)
            {
                _repository.Update(model);
                _repository.Save();
                return RedirectToAction("Index");
            }
            return View(model);
            
        }

        [HttpPost]
        public IActionResult Delete(Product model)
        {
            _repository.Delete(model.ProductId);
            _repository.Save();
            return RedirectToAction("Index");
        }
    }
}
