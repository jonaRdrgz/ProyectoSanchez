using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoSanchez.ViewModels
{
    public class TituloVM
    {
        [Required(ErrorMessage = "El id del título es obligatorio")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Sólo datos numéricos son permitidos")]
        public short IdTitulo { get; set; }

        [Required(ErrorMessage = "Es necesaria la descripción del título")]
        [StringLength(40,ErrorMessage ="Descripción del título muy extensa")]
        public string Descripcion { get; set; }

        public List<TituloPorEntrenadorVM> TituloPorEntrenadors { get; set; }
    }
}