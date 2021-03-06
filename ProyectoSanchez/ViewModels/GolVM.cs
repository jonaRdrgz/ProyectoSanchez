﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoSanchez.ViewModels
{
    public class GolVM
    {
        //[Required(ErrorMessage = "El identificador del gol es obligatorio")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Sólo datos numéricos son permitidos")]
        public decimal IdGol { get; set; }

        [Required(ErrorMessage = "El id del jugador es obligatorio")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Sólo datos numéricos son permitidos")]
        public decimal IdJugador { get; set; }

        [Required(ErrorMessage = "El minuto es necesario")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Sólo datos numéricos son permitidos")]
        public decimal Minuto { get; set; }

        [Required(ErrorMessage = "El id del equipo es obligatorio")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Sólo datos numéricos son permitidos")]
        public decimal IdEquipo { get; set; }

        [StringLength(200, ErrorMessage = "El URL es muy extenso")]
        public string Url { get; set; }

        [Required(ErrorMessage = "El id del partido es obligatorio")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Sólo datos numéricos son permitidos")]
        public decimal IdPartido { get; set; }
    }
}