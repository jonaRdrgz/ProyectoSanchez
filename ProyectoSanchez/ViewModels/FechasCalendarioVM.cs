using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoSanchez.ViewModels
{
    public class FechasCalendarioVM
    {
        [Required(ErrorMessage = "El id de la fecha es obligatoria")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Sólo datos numéricos son permitidos")]
        public int IdFecha { get; set; }
        [Required(ErrorMessage = "La fecha y hora es obligatoria")]
        public System.DateTime FechaProgramada { get; set; }

        [Required(ErrorMessage = "El id del torneo es obligatorio")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Sólo datos numéricos son permitidos")]
        public int IdTorneo { get; set; }

        public List<PartidoVM> Partidoes { get; set; }
    }
}