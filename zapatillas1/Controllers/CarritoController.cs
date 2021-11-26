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

            int prodId = Convert.ToInt32(id);
            int cantCompra = Convert.ToInt32(cantidad);

            Producto p = Carrito.buscarProducto(prodId, Carrito.ListaStock);

            p.Cantidad -= cantCompra;
            p.Cantidad_compra += cantCompra;
            Carrito.bolsaCompra.Add(p);

            return Json(data);

            // return RedirectToAction(nameof(Ver));

        }

        public async Task<IActionResult> Ver()
        {
            return View();
        }

        // probar con ajax        
        public async Task<IActionResult> Finalizar()
        {

            // crear lista de compra y enviar

            foreach (Producto p in Carrito.bolsaCompra)
            {
                p.Cantidad_compra = 0;
                // ver adonde se esta restando la cantidad
                _context.Update(p);
                await _context.SaveChangesAsync();
                // agregar a venta

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
