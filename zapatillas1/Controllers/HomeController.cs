using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using zapatillas1.zapatillas1.Models;
using zapatillas1.zapatillas1.Data;


namespace zapatillas1.Controllers
{
    public class HomeController : Controller
    {

        private readonly EshopDbContext db;
        public HomeController(EshopDbContext context)
        {
            db = context;
        }

        public IActionResult Index()
        {
            ViewBag.name = "Jose";
            ViewBag.productos = db.Productos.ToList();

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
