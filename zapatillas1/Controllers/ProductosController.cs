using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using zapatillas1.zapatillas1.Data;
using zapatillas1.zapatillas1.Models;
using static zapatillas1.zapatillas1.Models.Carrito;

namespace zapatillas1.Controllers
{
    public class ProductosController : Controller
    {
        private readonly EshopDbContext _context;

        public ProductosController(EshopDbContext context)
        {
            _context = context;
        }

        // GET: Productos
        public async Task<IActionResult> Index()
        {


            //PARA LA VISTA
            // agrupando por cod_producto para no repetir items

            if (Carrito.primeraVez)
            {

                //para manejar el carrito utilizamos lista estaticas (para que duren con la sesion) y al finalizar la compra sincronizamos estas listas con la BD.
                //recibo una una lista de todos los productos (trae todo el modelo de la db)
                var productos = await _context.Productos.ToListAsync();

                //agrupo por codigo de producto (pero solo uno para no repetir foto, pues tengo varios productos con mismo codProducto pero dif talle)
                var productosPorCodigo = productos.Where(p => p.Cantidad > 0).GroupBy(x => x.Cod_producto).Select(g => g.First());

                Carrito.ListaStock.Clear();

                foreach (Producto item in productosPorCodigo)
                {
                    Carrito.ListaStock.Add(item);
                }

                primeraVez = false;

            }

            //mando esta lista filtrada al viewBag. 
            ViewBag.productosPorCodigo = Carrito.ListaStock;

            return View();
            // return View(await _context.Productos.ToListAsync());
        }


        // GET: Productos/Stock
        public async Task<IActionResult> Stock()
        {

            return View(await _context.Productos.ToListAsync());
        }

        // GET: Productos/Details/FAFSF
        //public async Task<IActionResult> Details(string? codProducto)
        // public async Task<IActionResult> Details(int? id) //deberia recibir codProducto.  (int? id) recibia antes
        public async Task<IActionResult> Details(string id)
        {

            String codBuscado = id;

            if (codBuscado == null)
            {
                return NotFound();
            }

            ArrayList productosPorCodigo = Carrito.buscarProductoPorCodigo(codBuscado, Carrito.ListaStock);
            Producto producto = (Producto)productosPorCodigo[0];

            if (producto == null)
            {
                return NotFound();
            }
            else
            {
                ViewBag.talles = productosPorCodigo;
            }

            //en la vista en vez de agarrar id agarro codProducto
            return View(producto);


        }


        // GET: Productos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Productos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Cod_producto,Foto,Cantidad,Talle,Descripcion,Precio,En_stock")] Producto producto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(producto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(producto);
        }

        // GET: Productos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        // POST: Productos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Cod_producto,Foto,Cantidad,Talle,Descripcion,Precio,En_stock")] Producto producto)
        {
            if (id != producto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(producto);
                    await _context.SaveChangesAsync();
                    Carrito.primeraVez = true;
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductoExists(producto.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(producto);
        }

        // GET: Productos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        // POST: Productos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var producto = await _context.Productos.FindAsync(id);
            _context.Productos.Remove(producto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductoExists(int id)
        {
            return _context.Productos.Any(e => e.Id == id);
        }
    }
}
