using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoSanchez.ViewModels
{
    public class CategoriaVM
    {
        [Required(ErrorMessage="El id de la categoría es obligatorio")]
        public int IdCategoria { get; set; }
        [Required(ErrorMessage = "La descripción de la categoría es obligatorio")]
        [StringLength(25,ErrorMessage ="La descripción de la categoría es muy grande.")]
        public string Descripcion { get; set; }
    }
}