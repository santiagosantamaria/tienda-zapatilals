using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using zapatillas1.zapatillas1.Data;
using zapatillas1.zapatillas1.Models;
using Microsoft.AspNetCore.Authorization;

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
        [Authorize]
        public async Task<IActionResult> Index()
        {

            var ItemsVendidos = _context.Ventas.ToList();
            ViewBag.Ventas = ItemsVendidos;

            return View();
        }
    }
}
