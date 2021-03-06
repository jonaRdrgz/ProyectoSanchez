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
    
    public partial class Equipo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Equipo()
        {
            this.ContratacionFuncionarios = new HashSet<ContratacionFuncionario>();
            this.EquipoPorTorneos = new HashSet<EquipoPorTorneo>();
            this.OfertaFuncionarios = new HashSet<OfertaFuncionario>();
            this.Socios = new HashSet<Socio>();
        }
    
        public decimal idEquipo { get; set; }
        public string nombre { get; set; }
        public System.DateTime fechaFundacion { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ContratacionFuncionario> ContratacionFuncionarios { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EquipoPorTorneo> EquipoPorTorneos { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OfertaFuncionario> OfertaFuncionarios { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Socio> Socios { get; set; }
    }
}
