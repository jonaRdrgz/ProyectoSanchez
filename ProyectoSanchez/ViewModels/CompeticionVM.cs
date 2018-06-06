using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoSanchez.ViewModels
{
    public class CompeticionVM
    {
        [Required(ErrorMessage = "El id de la competición es obligatorio")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Solo datos numéricos son permitidos")]
        public int IdCompeticion { get; set; }
        [Required(ErrorMessage = "El nombre de la competición es obligatorio")]
        [StringLength(20,ErrorMessage ="El nombre de la competición es muy grande")]
        public string NombreCompeticion { get; set; }
        [Required(ErrorMessage = "El tipo de la competición es obligatorio")]
        [StringLength(10, ErrorMessage = "El tipo de la competición es muy grande")]
        public string Tipo { get; set; }
    }
}