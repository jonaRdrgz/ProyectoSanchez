using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoSanchez.ViewModels
{
    public class OfertaFuncionarioVM
    {
        [Required(ErrorMessage = "El código del funcionario es obligatorio")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Sólo datos numéricos son permitidos")]
        public int CodigoFuncionario { get; set; }

        [Required(ErrorMessage = "El código del equipo es obligatorio")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Sólo datos numéricos son permitidos")]
        public int IdEquipo { get; set; }

        //debo revisarlo
        public decimal Importe { get; set; }

        [Required(ErrorMessage = "Se necesita la fecha de la oferta")]
        public Nullable<System.DateTime> fecha { get; set; }

        [StringLength(40,ErrorMessage ="Descripción de la oferta muy extensa")]
        public string Descripcion { get; set; }
    }
}