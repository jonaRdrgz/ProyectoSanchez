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
    
    public partial class ArbitroPorPartido
    {
        public decimal idArbitro { get; set; }
        public decimal idPartido { get; set; }
        public decimal desempenno { get; set; }
        public decimal idTipoArbitro { get; set; }
    
        public virtual Arbitro Arbitro { get; set; }
        public virtual Partido Partido { get; set; }
        public virtual TipoArbitro TipoArbitro { get; set; }
    }
}
