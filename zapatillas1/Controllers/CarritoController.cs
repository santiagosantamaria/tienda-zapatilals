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

            if (Carrito.buscarProducto(prodId, Carrito.bolsaCompra) == null)
            {
                Carrito.bolsaCompra.Add(p);
            }

            return Json(data);

        }

        public async Task<IActionResult> Ver()
        {
            return View();
        }


        public async Task<IActionResult> Finalizar()
        {


            // agregar a ventas los items de bolsaCompra, 
            // el string de una compra entera
            // acumular  cant items, precio, string de compra, guardar en ventas
            String descVenta = "";

            foreach (Producto p in Carrito.bolsaCompra)
            {
                descVenta += "Codigo: " + p.Cod_producto + " . " + p.Cantidad_compra + " | " + " ";

                p.Cantidad_compra = 0;
                _context.Update(p);
                await _context.SaveChangesAsync();

            }

            // Enviar Info a Ventas


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
