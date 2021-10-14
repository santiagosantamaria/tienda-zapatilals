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


        //    public string DbPath { get; private set; }


        //    public EshopDbContext()
        //    {
        //        // var folder = Environment.SpecialFolder.LocalApplicationData;
        //        // var path   = Environment.GetFolderPath(folder);

        //        // DbPath = $"{path}{System.IO.Path.DirectorySeparatorChar}" + "/Data/eshop.db";

        //        //DbPath = "C:/Users/Dibu/source/repos/zapatillas1/zapatillas1/Data/eshop.db";

        //    }

        //    // The following configures EF to create a Sqlite database file in the
        //    // special "local" folder for your platform.
        //    protected override void OnConfiguring(DbContextOptionsBuilder options)
        //        => options.UseSqlite($"Data Source={DbPath}");
        //}

        public EshopDbContext(DbContextOptions opciones) : base(opciones)        {        }



    }

}