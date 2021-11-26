using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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

            if (Carrito.primeraVez)
            {

                //para manejar el carrito utilizamos lista estaticas (para que duren con la sesion) y al finalizar la compra sincronizamos estas listas con la BD.
                //recibo una una lista de todos los productos (trae todo el modelo de la db)
                var productos = await _context.Productos.ToListAsync();

                Carrito.ListaStock.Clear();
                Carrito.ListaHomeProductos.Clear();

                //agrupo por codigo de producto (pero solo uno para no repetir foto, 
                // pues tengo varios productos con mismo codProducto pero dif talle)

                var totalItemesEnStock = _context.Productos.FromSqlRaw("Select Id, Cod_producto, Foto, Cantidad, Cantidad_compra, Talle, Descripcion, Precio, En_stock from Productos where Cantidad > 0").ToList();
                var productosPorCodigo = productos.Where(p => p.Cantidad > 0).GroupBy(x => x.Cod_producto).Select(g => g.First());

                // lista total del stock
                foreach (Producto item in totalItemesEnStock)
                {
                    Carrito.ListaStock.Add(item);
                }

                // lista para el view productos - agrupados por Cod_producto
                foreach (Producto item in productosPorCodigo)
                {
                    Carrito.ListaHomeProductos.Add(item);
                }

                Carrito.primeraVez = false;

            }

            Carrito.ListaHomeProductos.Clear();

            foreach (Producto item in Carrito.ListaStock)
            {
                Carrito.ListaHomeProductos.Add(item);
            }

            Carrito.ListaHomeProductos = Carrito.ListaHomeProductos.Where(p => p.Cantidad > 0).GroupBy(x => x.Cod_producto).Select(g => g.First()).ToList();

            //mando esta lista filtrada al viewBag. 
            ViewBag.productosPorCodigo = Carrito.ListaHomeProductos;

            return View();

        }


        // GET: Productos/Stock
        [Authorize]
        public async Task<IActionResult> Stock()
        {

            return View(await _context.Productos.ToListAsync());
        }

        // GET: Productos/Details/FAFSF
        //public async Task<IActionResult> Details(string? codProducto)

        public async Task<IActionResult> Details(String id)
        {

            String codBuscado = id;

            if (codBuscado == null)
            {
                return NotFound();
            }

            var totalTalles = _context.Productos.FromSqlRaw("Select Id, Cod_producto, Foto, Cantidad, Cantidad_compra, Talle, Descripcion, Precio, En_stock from Productos where Cantidad > 0 GROUP by Cod_producto,Talle")
            .ToList().Where(p => p.Cod_producto.Equals(codBuscado));

            List<Producto> productosPorCodigo = Carrito.buscarProductoPorCodigo(codBuscado);

            if (productosPorCodigo == null)
            {
                return NotFound();
            }

            ViewBag.totalTalles = totalTalles;

            Producto producto = productosPorCodigo[0];
            return View(producto);


        }

        [HttpGet]
        public ActionResult Cantidad(string id, string talle, string codp)
        {
            // debug
            // string saludo = "Hola tu id es: " + id + " | talle:  " + talle + " | codigo prod " + codp;

            var talleBuscado = (float)Convert.ToDouble(talle);
            int cantidad = 0;
            int idProducto = 0;
            string codProducto = "";


            foreach (Producto p in Carrito.ListaStock)
            {
                if (p.Cod_producto.Equals(codp) && p.Talle == talleBuscado)
                {
                    cantidad = p.Cantidad;
                    codProducto = p.Cod_producto;
                    idProducto = p.Id;
                }
            }

            string IdProductoString = idProducto.ToString();
            string cantidadString = cantidad.ToString();

            string[] data = new string[3] { IdProductoString, codProducto, cantidadString };

            return Json(data);

        }



        // GET: Productos/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Productos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Id,Cod_producto,Foto,Cantidad,Cantidad_compra,Talle,Descripcion,Precio,En_stock")] Producto producto)
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
        [Authorize]
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Cod_producto,Foto,Cantidad,Cantidad_compra,Talle,Descripcion,Precio,En_stock")] Producto producto)
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
                return RedirectToAction(nameof(Stock));

            }
            return View(producto);
        }

        // GET: Productos/Delete/5
        [Authorize]
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
        [Authorize]
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
