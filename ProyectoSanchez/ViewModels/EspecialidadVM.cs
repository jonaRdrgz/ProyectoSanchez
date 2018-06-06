using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoSanchez.ViewModels
{
    public class EspecialidadVM
    {
        [Required(ErrorMessage = "El id de la especialidad es obligatorio")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Sólo datos numéricos son permitidos")]
        public short IdEspecialidad { get; set; }

        [Required(ErrorMessage = "La descripción de la especialidad es obligatoria")]
        [StringLength(40,ErrorMessage = "La descripción es muy extensa")]
        public string Descripcion { get; set; }

        public List<EspecialidadPorJugadorVM> EspecialidadPorJugadors { get; set; }
    }
}