using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
namespace ProyectoSanchez.ViewModels
{
    public class FederacionVM
    {
        [Required(ErrorMessage = "El id de la federación es obligatorio")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Sólo datos numéricos son permitidos")]
        public short IdFederacion { get; set; }

        [Required(ErrorMessage = "El nombre de la federación es obligatorio")]
        [StringLength(40,ErrorMessage ="El nombre de la federación es muy extenso")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El id del país es obligatorio")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Sólo datos numéricos son permitidos")]
        public short IdPais { get; set; }

        [Required(ErrorMessage = "Es necesaria la fecha de creación de la federación")]
        public System.DateTime FechaCreacionFed { get; set; }

        public List<CompeticionPorFederacionVM> CompeticionPorFederacions { get; set; }
    }
}