using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoSanchez.ViewModels
{
    public class EquipoVM
    {
        [Required(ErrorMessage = "El ID del equipo es obligatorio")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Sólo datos numéricos son permitidos")]
        public int IdEquipo { get; set; }

        [Required(ErrorMessage = "El nombre del equipo es obligatorio")]
        [StringLength(30,ErrorMessage = "El nombre del equipo es muy extenso")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "La fecha de creación del equipo es obligatoria")]
        public System.DateTime FechaFundacion { get; set; }

        public List<ContratacionFuncionarioVM> ContratacionFuncionarios { get; set; }
        public List<EquipoPorTorneoVM> EquipoPorTorneos { get; set; }
        public List<OfertaFuncionarioVM> OfertaFuncionarios { get; set; }
    }
}