using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using zapatillas1.zapatillas1.Data;
using zapatillas1.zapatillas1.Models;

namespace zapatillas1.Controllers
{
    public class VentasController : Controller
    {
        private readonly EshopDbContext _context;

        public VentasController(EshopDbContext context)
        {
            _context = context;
        }

        // GET: Ventas
        public async Task<IActionResult> Index()
        {
            ViewBag.Productos = _context.Productos.ToArray();
            ViewBag.VentasXProductos = _context.VentaXProductos.ToArray();
            return View(await _context.Ventas.ToListAsync());
        }
    }
}
