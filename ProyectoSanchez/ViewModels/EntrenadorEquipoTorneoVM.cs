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
        public decimal IdEntrenador { get; set; }

        [Required(ErrorMessage = "El id del equipo es obligatorio")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Sólo datos numéricos son permitidos")]
        public decimal IdEquipo { get; set; }

        [Required(ErrorMessage = "El id del torneo es obligatorio")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Sólo datos numéricos son permitidos")]
        public decimal IdTorneo { get; set; }

        [RegularExpression("([0-9]+)", ErrorMessage = "Sólo datos numéricos son permitidos")]
        public decimal? Posicion { get; set; }

        [StringLength(50, ErrorMessage = "Sinopsis del entrenador muy extensa")]
        public string Sinopsis { get; set; }

        public string NombreTorneo { get; set; }

        public decimal Anno { get; set; }

        public string Tipo { get; set; }

        public string NombreEquipo { get; set; }

        public string Periodo { get; set; }

    }
}