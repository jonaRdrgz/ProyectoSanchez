using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoSanchez.ViewModels
{
    public class PartidoVM
    {
        [Required(ErrorMessage = "El id del partido es obligatorio")]
        //[RegularExpression("([0-9]+)", ErrorMessage = "Sólo datos numéricos son permitidos")]
        public decimal IdPartido { get; set; }

        [Required(ErrorMessage = "El id del equipo local es obligatorio")]
        //[RegularExpression("([0-9]+)", ErrorMessage = "Sólo datos numéricos son permitidos")]
        public decimal IdEquipoLocal { get; set; }

        [Required(ErrorMessage = "El id del equipo visita es obligatorio")]
        //[RegularExpression("([0-9]+)", ErrorMessage = "Sólo datos numéricos son permitidos")]
        public decimal IdEquipoVisita { get; set; }

        [Required(ErrorMessage = "Se necesita la cantidad de goles del equipo local")]
        //[RegularExpression("([0-9]+)", ErrorMessage = "Sólo datos numéricos son permitidos")]
        public ushort GolLocal { get; set; }

        [Required(ErrorMessage = "Se necesita la cantidad de goles del equipo visita")]
        //[RegularExpression("([0-9]+)", ErrorMessage = "Sólo datos numéricos son permitidos")]
        public ushort GolVisita { get; set; }

        [Required(ErrorMessage = "El id del torneo es obligatorio")]
        //[RegularExpression("([0-9]+)", ErrorMessage = "Sólo datos numéricos son permitidos")]
        public decimal IdTorneo { get; set; }

        [Required(ErrorMessage = "Es necesario saber si el juego se jugó o no")]
        public bool? Jugado { get; set; }

        [Required(ErrorMessage = "El id del la fecha es obligatorio")]
        //[RegularExpression("([0-9]+)", ErrorMessage = "Sólo datos numéricos son permitidos")]
        public decimal IdFecha { get; set; }
    }
}