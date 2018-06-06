using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoSanchez.ViewModels
{
    public class CambioVM
    {
        [Required(ErrorMessage = "El id del cambio es obligatorio")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Solo datos numéricos son permitidos")]
        public int IdCambio { get; set; }

        [Required(ErrorMessage = "El id del jugador entrante es obligatorio")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Solo datos numéricos son permitidos")]
        public int IdJugadorEntra { get; set; }
        [Required(ErrorMessage = "El id del jugador saliente es obligatorio")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Solo datos numéricos son permitidos")]
        public int IdJugadorSale { get; set; }
        [Required(ErrorMessage = "El minuto del cambio es obligatorio")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Solo datos numéricos son permitidos")]
        public ushort Minuto { get; set; }
        [Required(ErrorMessage = "El id del partido es necesario")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Solo datos numéricos son permitidos")]
        public int IdPartido { get; set; }
        [Required(ErrorMessage = "El id del equipo es necesario")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Solo datos numéricos son permitidos")]
        public int IdEquipo { get; set; }
    }
}