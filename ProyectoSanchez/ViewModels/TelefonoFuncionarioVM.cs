using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoSanchez.ViewModels
{
    public class TelefonoFuncionarioVM
    {
        [Required(ErrorMessage = "El código del funcionario es obligatorio")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Sólo datos numéricos son permitidos")]
        public int CodigoFuncionario { get; set; }

        [Required(ErrorMessage = "El número de teléfono del funcionario es obligatorio")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Sólo datos numéricos son permitidos")]
        public int Telefono { get; set; }
    }
}