using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoSanchez.ViewModels
{
    public class EntrenadorEquipoTorneoVM
    {
        [Required(ErrorMessage = "El id del entrenador es obligatorio")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Sólo datos numéricos son permitidos")]
        public int IdEntrenador { get; set; }

        [Required(ErrorMessage = "El id del equipo es obligatorio")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Sólo datos numéricos son permitidos")]
        public int IdEquipo { get; set; }

        [Required(ErrorMessage = "El id del torneo es obligatorio")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Sólo datos numéricos son permitidos")]
        public int IdTorneo { get; set; }

        [RegularExpression("([0-9]+)", ErrorMessage = "Sólo datos numéricos son permitidos")]
        public ushort Posicion { get; set; }

        [StringLength(50,ErrorMessage ="Sinopsis del entrenador muy extensa")]
        public string Sinopsis { get; set; }
    }
}