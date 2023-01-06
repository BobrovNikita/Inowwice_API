using Methanit_ASP_NET_Core_7.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Reflection.Metadata;

namespace Methanit_ASP_NET_Core_7.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationContext db;

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
        public IActionResult ScoreProcedure()
        {
            var a = db.Database.ExecuteSqlRaw("UpdateDefaultValueIntoFridgeProducts");
            return View("Index");
        }
    }
}