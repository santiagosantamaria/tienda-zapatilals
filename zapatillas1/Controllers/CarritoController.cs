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
    public class CarritoController : Controller
    {
        private readonly EshopDbContext _context;

        public CarritoController(EshopDbContext context)
        {
            _context = context;
        }

        // public async Task<IActionResult> Add(int? id)
        // Cod_producto-Talle
        public async Task<IActionResult> Add(string id)
        {
            string[] ids = id.Split('-');

            string codProducto = ids[0];
            int talle = Int32.Parse(ids[1]);

            var itemSeleccion = await _context.Productos.FirstOrDefaultAsync(m => m.Cod_producto == codProducto);

            ViewBag.itemSeleccion = itemSeleccion;

            return View();

        }


    }
}
