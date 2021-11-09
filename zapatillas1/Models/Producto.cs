using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace zapatillas1.zapatillas1.Models
{
    public class Producto
    {
        [Key]
        public int Id { get; set; }
        public string Cod_producto { get; set; }
        public string Foto { get; set; }
        public int Cantidad { get; set; }
        public float Talle { get; set; }
        public string Descripcion { get; set; }
        public float Precio { get; set; }
        public int En_stock { get; set; }



    }
}
