using System;
using System.Collections.Generic;
using System.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using zapatillas1.zapatillas1.Models;

namespace zapatillas1.zapatillas1.Data

{

    // THIS IS THE DB CONTEXT !!
    public class EshopDbContext : DbContext
    {
        public DbSet<Producto> Productos { get; set; }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Venta> Ventas { get; set; }
        public DbSet<VentaXProducto> VentaXProductos { get; set; }

        public EshopDbContext(DbContextOptions opciones) : base(opciones)
        {

        }



    }

}