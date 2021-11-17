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

        public static ArrayList bolsaCompra = new ArrayList();
        public static ArrayList ListaStock = new ArrayList();
        public static List<Producto> ListaHomeProductos = new List<Producto>();
        public static Boolean primeraVez = true;

        public static void agregarProductoAlCarrito(String codProducto, int talle)
        {
            Producto producto = buscarProducto(codProducto, talle, ListaStock);


            if (producto != null)
            {

                if (producto.Cantidad > 0)
                {
                    bolsaCompra.Add(producto);

                    //le resto en el stock una cantidad
                    producto.Cantidad--;
                }

            }
        }


        public static void removerProducto(int id, ArrayList lista)
        {
            Producto producto = buscarProducto(id, lista);

            if (producto != null)
            {
                bolsaCompra.Remove(producto);
                producto.Cantidad++;
            }

        }


        public static Producto buscarProducto(String codProducto, int talle, ArrayList lista)
        {
            int i = 0;
            Producto productobuscado = null;
            while (i < lista.Count && productobuscado == null)
            {
                Producto productoActual = (Producto)lista[i];

                if (productoActual.Cod_producto.Equals(codProducto) && productoActual.Talle == talle)
                {
                    productobuscado = productoActual;
                }
                else
                {
                    i++;
                }
            }

            return productobuscado;
        }

        public static Producto buscarProducto(int id, ArrayList lista)
        {
            int i = 0;
            Producto productobuscado = null;
            while (i < lista.Count && productobuscado == null)
            {
                Producto productoActual = (Producto)lista[i];

                if (productoActual.Id == id)
                {
                    productobuscado = productoActual;
                }
                else
                {
                    i++;
                }
            }

            return productobuscado;
        }


        public static List<Producto> buscarProductoPorCodigo(String codigo)
        {
            List<Producto> productosPorCodigo = new List<Producto>();

            foreach (Producto p in Carrito.ListaStock)
            {
                if (p.Cantidad > 0 && p.Cod_producto.Equals(codigo))
                {
                    productosPorCodigo.Add(p);
                }
            }


            return productosPorCodigo;
        }

        public static float getPrecioTotalItems()
        {
            float total = 0;
            foreach (Producto item in bolsaCompra)
            {
                total += item.Precio;
            }
            return total;
        }

    }
}

/*
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



   }*/
