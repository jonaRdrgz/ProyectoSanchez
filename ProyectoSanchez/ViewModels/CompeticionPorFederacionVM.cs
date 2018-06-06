using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoSanchez.ViewModels
{
    public class CompeticionPorFederacionVM
    {
        [Required(ErrorMessage = "El id de la federación es obligatorio")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Solo datos numéricos son permitidos")]
        public int IdFederacion { get; set; }

        [Required(ErrorMessage = "El id de la competición es obligatorio")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Solo datos numéricos son permitidos")]
        public int IdCompeticion { get; set; }

        public List<TorneoVM> Torneos { get; set; }
    }
}