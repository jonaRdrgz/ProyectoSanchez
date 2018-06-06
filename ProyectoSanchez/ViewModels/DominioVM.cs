using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoSanchez.ViewModels
{
    public class DominioVM
    {
        [Required(ErrorMessage = "El id del dominio es obligatorio")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Sólo datos numéricos son permitidos")]
        public ushort IdDominio { get; set; }

        [Required(ErrorMessage = "La descripción del dominio es obligatoria")]
        [StringLength(25, ErrorMessage = "La descripcion es muy extensa")]
        public string Descripcion { get; set; }

        public List<EspecialidadPorJugadorVM> EspecialidadPorJugadors { get; set; }
    }
}