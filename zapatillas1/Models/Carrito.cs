using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace zapatillas1.zapatillas1.Models
{
    public static class Carrito
    {

        public static ArrayList ListaProductos = new ArrayList();

        public static void addItem(Producto producto)
        {
            ListaProductos.Add(producto);
        }
        public static void removeItem(Producto producto)
        {
            ListaProductos.Remove(producto);
        }
        public static float getPrecioTotalItems()
        {
            float total = 0;
            foreach (Producto item in ListaProductos)
            {
                total += item.Precio;
            }
            return total;
        }



    }

}
