using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoSanchez.ViewModels
{
    public class DireccionFuncionarioVM
    {
        [Required(ErrorMessage = "El id del funcionario es obligatorio")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Sólo datos numéricos son permitidos")]
        public int CodigoFuncionario { get; set; }

        [Required(ErrorMessage = "El correo del funcionario es obligatorio")]
        [StringLength(40, ErrorMessage = "La direccion es muy extensa")]
        public string Direccion { get; set; }
    }
}