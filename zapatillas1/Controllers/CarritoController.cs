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



        // public async Task<IActionResult> Add(int? id)
        // Cod_producto-Talle
        public async Task<IActionResult> Add(string id)
        {

            string[] ids = id.Split('-');

            string codProducto = ids[0];
            int talle = Int32.Parse(ids[1]);

            Carrito.agregarProductoAlCarrito(codProducto, talle);

            /*
            var itemSeleccion = await _context.Productos.FirstOrDefaultAsync(m => m.Cod_producto == codProducto);

            ViewBag.itemSeleccion = itemSeleccion;
            Carrito.ListaProductos.Add(itemSeleccion);
            */

            return RedirectToAction(nameof(Ver));

        }

        public async Task<IActionResult> Ver()
        {
            return View();
        }

        public async Task<IActionResult> Finalizar()
        {
            for (int i = 0; i < Carrito.bolsaCompra.Count; i++)
            {
                Producto prod = (Producto)Carrito.bolsaCompra[i];
                Carrito.removerProducto(prod.Id, Carrito.bolsaCompra);

                var producto = await _context.Productos.FirstOrDefaultAsync(m => m.Id == prod.Id);
                producto.Cantidad--;
                _context.Update(producto);
                await _context.SaveChangesAsync();
            }

            //   buscar en la bd la zapatilla que tenga ese talle y ese codProducto y restarle 1 a la cantidad



            Carrito.primeraVez = true;

            return RedirectToAction(nameof(Ver));
        }

        public async Task<IActionResult> Remover(int id)
        {
            // Carrito.removeItem(id);
            Carrito.removerProducto(id, Carrito.bolsaCompra);
            return RedirectToAction(nameof(Ver));
        }





    }
}
