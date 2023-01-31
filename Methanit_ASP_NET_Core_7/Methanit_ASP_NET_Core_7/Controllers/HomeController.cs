using Methanit_ASP_NET_Core_7.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Methanit_ASP_NET_Core_7.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationContext db;

        public HomeController(ApplicationContext context)
        {
            db = context;

        }
        [HttpGet]
        public IActionResult Index()
        {
            return View("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public IActionResult ScoredProcedure()
        {
            var a = db.Database.ExecuteSqlRaw("UpdateDefaultValueIntoFridgeProducts");
            return View("Index");
        }
    }
}