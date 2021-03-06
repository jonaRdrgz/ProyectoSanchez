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
    
    public partial class Contrato
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Contrato()
        {
            this.ContratacionFuncionarios = new HashSet<ContratacionFuncionario>();
            this.Entrenadors = new HashSet<Entrenador>();
            this.Jugadors = new HashSet<Jugador>();
        }
    
        public decimal idContrato { get; set; }
        public decimal salario { get; set; }
        public string moneda { get; set; }
        public string descripcion { get; set; }
        public System.DateTime fechaInicio { get; set; }
        public System.DateTime fechaSalida { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ContratacionFuncionario> ContratacionFuncionarios { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Entrenador> Entrenadors { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Jugador> Jugadors { get; set; }
    }
}
