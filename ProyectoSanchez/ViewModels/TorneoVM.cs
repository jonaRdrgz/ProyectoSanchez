using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoSanchez.ViewModels
{
    public class TorneoVM
    {
        [Required(ErrorMessage = "El id del torneo es obligatorio")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Sólo datos numéricos son permitidos")]
        public int IdTorneo { get; set; }

        [Required(ErrorMessage = "El id de la competición es obligatorio")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Sólo datos numéricos son permitidos")]
        public int IdCompeticion { get; set; }

        [Required(ErrorMessage = "El id de la federación es obligatorio")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Sólo datos numéricos son permitidos")]
        public short IdFederacion { get; set; }

        [Required(ErrorMessage = "El año del torneo es obligatorio")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Sólo datos numéricos son permitidos")]
        public short Anno { get; set; }

        [Required(ErrorMessage = "El periodo del torneo es obligatorio")]
        [StringLength(15,ErrorMessage = "El periodo posee un tamaño muy extenso")]
        public string Periodo { get; set; }

        public List<EquipoPorTorneoVM> EquipoPorTorneos { get; set; }
        public List<FechasCalendarioVM> FechasCalendarios { get; set; }
    }
}