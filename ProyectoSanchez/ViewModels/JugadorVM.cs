using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoSanchez.ViewModels
{
    public class JugadorVM
    {
        [Required(ErrorMessage = "El id del jugador es obligatorio")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Sólo datos numéricos son permitidos")]
        public decimal IdJugador { get; set; }

        [Required(ErrorMessage = "El id del funcionario es obligatorio")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Sólo datos numéricos son permitidos")]
        public decimal CodigoFuncionario { get; set; }

        [Required(ErrorMessage = "El peso del jugador es necesario")]
        public decimal PesoKilos { get; set; }

        [Required(ErrorMessage = "La altura del jugador es necesaria")]
        public decimal AlturaMetros { get; set; }

        public string Nombre { get; set; }
        public List<EspecialidadPorJugadorVM> EspecialidadPorJugadors { get; set; }
        public List<JugadorPorEquipoPorTorneoVM> JugadorPorEquipoPorTorneos { get; set; }
        public List<ContratoVM> Contratoes { get; set; }
    }
}