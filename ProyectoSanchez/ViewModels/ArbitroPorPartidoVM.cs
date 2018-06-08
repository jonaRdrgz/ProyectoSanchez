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
        public decimal IdArbitro { get; set; }

        [Required(ErrorMessage = "El id del partido debe ser obligatorio.")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Solo datos numéricos son permitidos")]
        public decimal IdPartido { get; set; }
        [Required(ErrorMessage = "El desempeño es obligatorio")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Solo datos numéricos son permitidos")]
        public decimal Desempenno { get; set; }

        [Required(ErrorMessage ="El tipo de árbitro es obligatorio")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Solo datos numéricos son permitidos")]
        public decimal IdTipoArbitro { get; set; }

        public string DescripcionTA { get; set; }

        public string Nombre { get; set; }
    }
}