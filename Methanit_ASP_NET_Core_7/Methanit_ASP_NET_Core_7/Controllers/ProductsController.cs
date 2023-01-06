using Methanit_ASP_NET_Core_7.Models;
using Methanit_ASP_NET_Core_7.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Methanit_ASP_NET_Core_7.Controllers
{
    public class ProductsController : Controller
    {
        private IRepository<Products> _repository;
        public ProductsController(IRepository<Products> repository)
        {
            _repository = repository;
        }

        //Get methods
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
                Products? model = _repository.GetModel((Guid)id);
                if (model != null)
                    return View(model);
            }

            return NotFound();
        }

        public IActionResult Deletes(Guid? id)
        {
            if (id != null)
            {
                Products? model = _repository.GetModel((Guid)id);
                return PartialView("Deletes", model);
            }
            else
            {
                return NotFound();
            }
        }
        //Post methods
        [HttpPost]
        public IActionResult Create(Products model)
        {
            if (ModelState.IsValid)
            {
                _repository.Create(model);
                _repository.Save();
                return RedirectToAction("Index");
            }
            else
            {
                return View(model);
            }
            
        }

        [HttpPost]
        public IActionResult Edit(Products model)
        {
            if (ModelState.IsValid)
            {
                _repository.Update(model);
                _repository.Save();
                return RedirectToAction("Index");
            }
            else
            {
                return View(model);
            }
            
        }

        [HttpPost]
        public IActionResult Delete(Products model)
        {
            _repository.Delete(model.ProductsId);
            _repository.Save();
            return RedirectToAction("Index");
        }
    }
}
