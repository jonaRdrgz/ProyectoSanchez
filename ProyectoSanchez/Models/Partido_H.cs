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
    
    public partial class Partido_H
    {
        public System.DateTime fecha_cambio { get; set; }
        public string usuario_cambio { get; set; }
        public string tipo_cambio { get; set; }
        public decimal idPartido { get; set; }
        public decimal idEquipoLocal { get; set; }
        public decimal idEquipoVisita { get; set; }
        public decimal golLocal { get; set; }
        public decimal golVisita { get; set; }
        public decimal idTorneo { get; set; }
        public string jugado { get; set; }
        public decimal idFecha { get; set; }
    }
}
