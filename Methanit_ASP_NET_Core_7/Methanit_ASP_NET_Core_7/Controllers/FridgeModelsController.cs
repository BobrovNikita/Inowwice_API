using Methanit_ASP_NET_Core_7.Models;
using Methanit_ASP_NET_Core_7.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Methanit_ASP_NET_Core_7.Controllers
{
    public class FridgeModelsController : Controller
    {
        private readonly IRepository<Fridge_Model> _repository;

        public FridgeModelsController(IRepository<Fridge_Model> repository)
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
                Fridge_Model? model = _repository.GetModel((Guid)id);
                if (model != null)
                    return View(model);
            }
            return NotFound();
        }

        public IActionResult Deletes(Guid? id)
        {
            if (id != null)
            {
                Fridge_Model? model = _repository.GetModel((Guid)id);
                return PartialView("Deletes", model);
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult Edit(Fridge_Model model)
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
        public IActionResult Create(Fridge_Model model)
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
        public IActionResult Delete(Fridge_Model model)
        {
            _repository.Delete(model.Fridge_ModelId);
            _repository.Save();
            return RedirectToAction("Index");
        }
    }
}