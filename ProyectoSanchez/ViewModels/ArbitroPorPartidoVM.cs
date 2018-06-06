using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoSanchez.ViewModels
{
    public class ArbitroPorPartidoVM
    {
        [Required(ErrorMessage = "El id del árbitro debe ser obligatorio.")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Solo datos numéricos son permitidos")]
        public int IdArbitro { get; set; }

        [Required(ErrorMessage = "El id del partido debe ser obligatorio.")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Solo datos numéricos son permitidos")]
        public int IdPartido { get; set; }
        [Required(ErrorMessage = "El desempeño es obligatorio")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Solo datos numéricos son permitidos")]
        public int Desempenno { get; set; }

        [Required(ErrorMessage ="El tipo de árbitro es obligatorio")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Solo datos numéricos son permitidos")]
        public int IdTipoArbitro { get; set; }
    }
}