using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoSanchez.ViewModels
{
    public class ContratoVM
    {
        [Required(ErrorMessage = "El id del contrato es obligatorio")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Sólo datos numéricos son permitidos")]
        public int IdContrato { get; set; }
        //también debo revisar este numeric(6,2)
        [Required(ErrorMessage = "El salario es obligatorio")]
        public decimal Salario { get; set; }
        [Required(ErrorMessage = "Es necesaria la moneda del salario")]
        public string Moneda { get; set; }
        public string Descripcion { get; set; }
        [Required(ErrorMessage = "La fecha es obligatoria")]
        public System.DateTime FechaInicio { get; set; }
        [Required(ErrorMessage = "La fecha es obligatoria")]
        public System.DateTime FechaSalida { get; set; }

        public List<ContratacionFuncionarioVM> ContratacionFuncionarios { get; set; }
        public List<EntrenadorVM> Entrenadors { get; set; }
        public List<JugadorVM> Jugadors { get; set; }
    }
}