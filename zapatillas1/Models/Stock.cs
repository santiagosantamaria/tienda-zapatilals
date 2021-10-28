using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace zapatillas1.zapatillas1.Models
{
    public class Stock
    {
        [Key]
        public int id_producto { get; set; }
        public bool hay_stock { get; set; }



    }
}
