using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using zapatillas1.zapatillas1.Data;
using zapatillas1.zapatillas1.Models;
using static zapatillas1.zapatillas1.Models.Carrito;
using System.Web;
using Microsoft.AspNetCore.Http;


namespace zapatillas1.Controllers
{
    public class CarritoController : Controller
    {
        private readonly EshopDbContext _context;

        public CarritoController(EshopDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public ActionResult Add(string id, string cantidad)
        {
            string[] data = new string[2] { "este id: " + id, "cantidad: " + cantidad };
            return Json(data);

            // restar la cantidad a cantidad de ese producto
            // guardar la compra

            // return RedirectToAction(nameof(Ver));

        }

        public async Task<IActionResult> Ver()
        {
            return View();
        }

        // probar con ajax        
        public async Task<IActionResult> Finalizar()
        {

            foreach (Producto p in Carrito.bolsaCompra)
            {
                // ver adonde se esta restando la cantidad
                _context.Update(p);
                await _context.SaveChangesAsync();
            }


            Carrito.bolsaCompra.Clear();
            Carrito.primeraVez = true;

            return RedirectToAction(nameof(Ver));
        }

        public async Task<IActionResult> Remover(int id)
        {
            Carrito.removerProducto(id, Carrito.bolsaCompra);
            return RedirectToAction(nameof(Ver));
        }





    }
}
