using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoSanchez.ViewModels
{
    public class TituloPorEntrenadorVM
    {
        [Required(ErrorMessage = "El id del título es obligatorio")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Sólo datos numéricos son permitidos")]
        public short IdTitulo { get; set; }

        [Required(ErrorMessage = "El id del entrenador es obligatorio")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Solo datos numéricos son permitidos")]
        public int IdEntrenador { get; set; }

        [Required(ErrorMessage = "El ID del equipo es obligatorio")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Sólo datos numéricos son permitidos")]
        public int IdEquipo { get; set; }

        [Required(ErrorMessage = "El ID del torneo es obligatorio")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Sólo datos numéricos son permitidos")]
        public int IdTorneo { get; set; }
    }
}