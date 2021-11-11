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

        public string Email { get; set; }

        public string Password { get; set; }
        
        

    }
}
