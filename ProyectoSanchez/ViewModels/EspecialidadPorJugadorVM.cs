using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
namespace ProyectoSanchez.ViewModels
{
    public class EspecialidadPorJugadorVM
    {
        [Required(ErrorMessage = "El id del jugador es obligatorio")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Sólo datos numéricos son permitidos")]
        public int IdJugador { get; set; }

        [Required(ErrorMessage = "El id de la especialidad es obligatorio")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Sólo datos numéricos son permitidos")]
        public ushort IdEspecialidad { get; set; }

        [Required(ErrorMessage = "El id del dominio es obligatorio")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Sólo datos numéricos son permitidos")]
        public ushort IdDominio { get; set; }
    }
}