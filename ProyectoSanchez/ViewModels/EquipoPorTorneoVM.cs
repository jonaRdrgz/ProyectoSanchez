using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoSanchez.ViewModels
{
    public class EquipoPorTorneoVM
    {
        [Required(ErrorMessage = "El id del equipo es obligatorio")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Sólo datos numéricos son permitidos")]
        public int IdEquipo { get; set; }

        [Required(ErrorMessage = "El id del torneo es obligatorio")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Sólo datos numéricos son permitidos")]
        public int IdTorneo { get; set; }

        [Required(ErrorMessage = "La cantidad de puntos son necesarios")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Sólo datos numéricos son permitidos")]
        public short Puntos { get; set; }

        public List<EntrenadorEquipoTorneoVM> EntrenadorEquipoTorneos { get; set; }
        public List<JugadorPorEquipoPorTorneoVM> JugadorPorEquipoPorTorneos { get; set; }
        public List<PartidoVM> Partidoes { get; set; }
        public List<PartidoVM> Partidoes1 { get; set; }
        public List<TituloPorEntrenadorVM> TituloPorEntrenadors { get; set; }
    }
}