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
    
    public partial class EquipoPorTorneo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EquipoPorTorneo()
        {
            this.EntrenadorEquipoTorneos = new HashSet<EntrenadorEquipoTorneo>();
            this.JugadorPorEquipoPorTorneos = new HashSet<JugadorPorEquipoPorTorneo>();
            this.Partidoes = new HashSet<Partido>();
            this.Partidoes1 = new HashSet<Partido>();
        }
    
        public decimal idEquipo { get; set; }
        public decimal idTorneo { get; set; }
        public decimal puntos { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EntrenadorEquipoTorneo> EntrenadorEquipoTorneos { get; set; }
        public virtual Equipo Equipo { get; set; }
        public virtual Torneo Torneo { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<JugadorPorEquipoPorTorneo> JugadorPorEquipoPorTorneos { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Partido> Partidoes { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Partido> Partidoes1 { get; set; }
    }
}
