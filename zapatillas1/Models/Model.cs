using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace zapatillas1.zapatillas1.Models
{

    // THIS IS THE DB CONTEXT !!
    public class EshopContext : DbContext
    {
        public DbSet<Producto> Productos { get; set; }


        public string DbPath { get; private set; }



        public EshopContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            //DbPath = $"{path}{System.IO.Path.DirectorySeparatorChar}./Data/eshop.db";
            //DbPath = $"/Data/eshop.db";
            DbPath = path + "/Data/eshop.db";
        }

        // The following configures EF to create a Sqlite database file in the
        // special "local" folder for your platform.
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");
    }


}