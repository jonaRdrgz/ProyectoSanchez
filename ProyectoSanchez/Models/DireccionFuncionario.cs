//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ProyectoSanchez.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class DireccionFuncionario
    {
        public decimal codigoFuncionario { get; set; }
        public string direccion { get; set; }
    
        public virtual Funcionario Funcionario { get; set; }
    }
}
