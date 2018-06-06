using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoSanchez.ViewModels
{
    public class FuncionarioVM
    {
        [Required(ErrorMessage = "El id del funcionario es obligatorio")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Sólo datos numéricos son permitidos")]
        public int CodigoFuncionario { get; set; }

        [Required(ErrorMessage = "Nombre del funcionario es necesario")]
        [StringLength(50,ErrorMessage = "Nombre muy extenso")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "La fecha de nacimiento del funcionario es necesaria")]
        public System.DateTime FechaNacimiento { get; set; }

        public List<ContratacionFuncionarioVM> ContratacionFuncionarios { get; set; }
        public List<CorreoFuncionarioVM> CorreoFuncionarios { get; set; }
        public List<DireccionFuncionarioVM> DireccionFuncionarios { get; set; }
        public List<EntrenadorVM> Entrenadors { get; set; }
        public List<JugadorVM> Jugadors { get; set; }
        public List<TelefonoFuncionarioVM> TelefonoFuncionarios { get; set; }
    }
}