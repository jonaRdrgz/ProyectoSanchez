using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoSanchez.ViewModels
{
    public class EntrenadorVM
    {
        [Required(ErrorMessage = "El id del entrenador es obligatorio")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Solo datos numéricos son permitidos")]
        public decimal IdEntrenador { get; set; }

        [Required(ErrorMessage = "El id del funcionario es obligatorio")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Solo datos numéricos son permitidos")]
        public decimal CodigoFuncionario { get; set; }

        [Required(ErrorMessage = "La fecha es obligatoria")]
        public System.DateTime FechaInicio { get; set; }

        public string NombreEntrenador { get; set; }

        public List<EntrenadorEquipoTorneoVM> EntrenadorEquipoTorneos { get; set; }
        public List<TituloPorEntrenadorVM> TituloPorEntrenadors { get; set; }
        public List<ContratoVM> Contratoes { get; set; }

        public DateTime FechaNacimiento {get ; set;}
    }
}