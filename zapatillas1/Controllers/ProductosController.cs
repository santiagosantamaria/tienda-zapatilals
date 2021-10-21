using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using zapatillas1.zapatillas1.Models;
using zapatillas1.zapatillas1.Data;
using System.Linq;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace zapatillas1.Controllers
{
    public class ProductosController : Controller
    {
        private readonly EshopDbContext db;
        public ProductosController(EshopDbContext context)
        {
            db = context;
        }

        public IActionResult Index()
        {
            ViewBag.productos = db.Productos.ToList();
            return View();
        }
    }
}
