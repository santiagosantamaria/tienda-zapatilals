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



        public static void removerProducto(int id, ArrayList lista)
        {
            Producto producto = buscarProducto(id, lista);

            if (producto != null)
            {
                lista.Remove(producto);
                producto.Cantidad += producto.Cantidad_compra;
                producto.Cantidad_compra = 0;
            }

        }


        public static Producto buscarProducto(String codProducto, int talle, ArrayList lista)
        {
            int i = 0;
            Producto productobuscado = null;
            Boolean encontrado = false;

            while (i < lista.Count && !encontrado)
            {
                Producto productoActual = (Producto)lista[i];

                if (productoActual.Cod_producto.Equals(codProducto) && productoActual.Talle == talle)
                {
                    productobuscado = productoActual;
                    encontrado = true;
                }

                i++;

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
            foreach (Producto item in Carrito.bolsaCompra)
            {
                total += item.Precio * item.Cantidad_compra;
            }
            return total;
        }

        public static string obtenerDescripcionBolsa()
        {
            string desc = "";

            foreach (Producto item in Carrito.bolsaCompra)
            {
                desc += item.Cantidad_compra + " " + item.Descripcion + " Talle: " + item.Talle + " || ";
            }

            return desc;
        }

    }
}
