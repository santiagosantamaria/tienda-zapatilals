using System;
namespace zapatillas_demo_1.Models
{
    public class Producto
    {
     
        public int id_producto { get; set; }
        public String cod_producto { get; set; }
        public String foto { get; set; }
        public int cantidad { get; set; }
        public float talle { get; set; }
        public String descripcion { get; set; }
        public float precio { get; set; }
        public float en_stock { get; set; }


        public Producto()
        {
        }
    }
}
