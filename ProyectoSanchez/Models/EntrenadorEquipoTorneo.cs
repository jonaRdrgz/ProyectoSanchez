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
    
    public partial class EntrenadorEquipoTorneo
    {
        public decimal idEntrenador { get; set; }
        public decimal idEquipo { get; set; }
        public decimal idTorneo { get; set; }
        public Nullable<decimal> posicion { get; set; }
        public string sinopsis { get; set; }
    
        public virtual Entrenador Entrenador { get; set; }
        public virtual EquipoPorTorneo EquipoPorTorneo { get; set; }
    }
}