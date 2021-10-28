using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace zapatillas1.zapatillas1.Models
{
    public class Usuario
    {
        [Key]
        public int Id_usuario { get; set; }
        public int Id_rol { get; set; }
        public String email { get; set; }
        public String password { get; set; }
                            
    }
}
