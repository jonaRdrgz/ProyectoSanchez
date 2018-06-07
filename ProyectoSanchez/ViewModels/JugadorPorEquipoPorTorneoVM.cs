using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoSanchez.ViewModels
{
    public class JugadorPorEquipoPorTorneoVM
    {
        [Required(ErrorMessage = "El id del equipo es obligatorio")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Sólo datos numéricos son permitidos")]
        public decimal IdEquipo { get; set; }

        [RegularExpression("([0-9]+)", ErrorMessage = "Sólo datos numéricos son permitidos")]
        public decimal? Posicion { get; set; }

        [Required(ErrorMessage = "El id del jugador es obligatorio")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Sólo datos numéricos son permitidos")]
        public decimal IdJugador { get; set; }

        [Required(ErrorMessage = "El id del torneo es obligatorio")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Sólo datos numéricos son permitidos")]
        public decimal IdTorneo { get; set; }

        [StringLength(100,ErrorMessage ="La evaluación es muy extensa")]
        public string Evaluacion { get; set; }

        public string TipoTorneo { get; set; }

        public decimal Anno { get; set; }

        public string Periodo { get; set; }

        public string NombreEquipo { get; set; }
    }
}