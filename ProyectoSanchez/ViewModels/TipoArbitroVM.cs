using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoSanchez.ViewModels
{
    public class TipoArbitroVM
    {
        [Required(ErrorMessage = "El id del tipo del árbitro es obligatorio")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Sólo datos numéricos son permitidos")]
        public ushort IdTipoArbitro { get; set; }

        [Required(ErrorMessage = "La descripción del tipo del árbitro es obligatoria")]
        [StringLength(20,ErrorMessage ="La descripción es muy extensa")]
        public string Descripcion { get; set; }

        public List<ArbitroPorPartidoVM> ArbitroPorPartidoes { get; set; }
    }
}