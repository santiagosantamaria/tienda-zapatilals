using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;

namespace zapatillas1.zapatillas1.Models
{
    public class Rol
    {
        [Key]
        public int Id { get; set; }
        public int Id_Usuario { get; set; }
        public int Permisos { get; set; }


    }
}