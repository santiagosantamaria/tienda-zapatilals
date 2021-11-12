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
        public static ArrayList ListaStock = new ArrayList();

        public static void addItem(int idProducto)
        {
            Producto producto = Carrito.buscarProducto(idProducto);
            if (producto == null)
            {
                ListaProductos.Add(producto);
            }
        }
        public static void removeItem(int idProducto)
        {
            Producto producto = Carrito.buscarProducto(idProducto);

            if (producto != null)
            {
                ListaProductos.Remove(producto);
            }
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


        public static Producto buscarProducto(int id)
        {
            Producto prodBuscado = null;
            foreach (Producto item in ListaProductos)
            {
                if (item.Id == id)
                {
                    prodBuscado = item;
                }
            }

            return prodBuscado;

        }



    }

}
