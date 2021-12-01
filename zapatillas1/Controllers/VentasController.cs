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
            var totalProductos = _context.Productos.ToList();
            ViewBag.totalProductos = totalProductos;

            var ItemsVendidos = _context.Ventas.ToList();
            ViewBag.Ventas = ItemsVendidos;

            return View();
        }

        [Authorize]
        // public async Task<IActionResult> Zapatilla(int prodId)
        public async Task<IActionResult> Zapatilla(int id)
        {
            int prodId = id;

            Producto producto = _context.Productos.Where(p => p.Id == prodId).First();
            ViewBag.Producto = producto;

            // join ventaXproducto con ventas 
            var ItemsVendidos = _context.VentaXProductos.Join(
                _context.Ventas,
                ventaProd => ventaProd.Id_Venta,
                venta => venta.Id_Venta,
                (ventaProd, venta) => new RegistroVenta()
                {
                    Id_Producto = ventaProd.Id_Producto,
                    CantVenta = ventaProd.Cantidad,
                    Fecha = venta.Fecha,

                }).ToList().Where(v => v.Id_Producto == prodId);


            ViewBag.Ventas = ItemsVendidos;

            return View();
        }
    }
}
