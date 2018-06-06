using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoSanchez.ViewModels
{
    public class UsuarioLoginVM
    {
        [Required(ErrorMessage = "El nombre es usuario es requerido")]
        public string Usuario { get; set; }

        [Required(ErrorMessage = "La contraseña del usuario es obligatoria")]
        [DataType(DataType.Password)]
        public string Clave { get; set; }
    }
}