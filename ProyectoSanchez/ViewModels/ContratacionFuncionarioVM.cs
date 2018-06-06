using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoSanchez.ViewModels
{
    public class ContratacionFuncionarioVM
    {
        [Required(ErrorMessage ="El código del funcionario es obligatorio")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Solo datos numéricos son permitidos")]
        public int CodigoFuncionario { get; set; }

        [Required(ErrorMessage = "El código del equipo es obligatorio")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Solo datos numéricos son permitidos")]
        public int IdEquipo { get; set; }

        [Required(ErrorMessage = "El id del contrato es obligatorio")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Solo datos numéricos son permitidos")]
        public int IdContrato { get; set; }
    }
}