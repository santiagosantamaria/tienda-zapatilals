﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace zapatillas1.zapatillas1.Models
{
    public class VentaXProducto
    {
        [Key]
        public int Id_Venta { get; set; }
        [Key]
        public int Id_Producto { get; set; }
    }
}
