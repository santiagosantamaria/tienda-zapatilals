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

        public async Task<IActionResult> Add(int? id) //deberia recibir codProducto
        {
            // estaria bueno que lo agregue a la sesion
            
            var itemSeleccion = await _context.Productos.FirstOrDefaultAsync(m => m.Id == id && m.En_stock == 1);
            ViewBag.itemSeleccion = itemSeleccion;

            return View();

        }




    }
}
