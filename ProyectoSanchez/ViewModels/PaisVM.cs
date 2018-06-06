using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoSanchez.ViewModels
{
    public class PaisVM
    {
        [Required(ErrorMessage = "El id del país es obligatorio")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Sólo datos numéricos son permitidos")]
        public short IdPais { get; set; }

        [Required(ErrorMessage = "El nombre del país es obligatorio")]
        [StringLength(30,ErrorMessage = "El nombre del país es muy extenso")]
        public string Nombre { get; set; }

        public List<FederacionVM> Federacions { get; set; }
    }
}