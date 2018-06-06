using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoSanchez.ViewModels
{
    public class ArbitroVM
    {
        [Required(ErrorMessage="El id del árbitro es obligatorio.")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Solo datos numéricos son permitidos")]
        public int IdArbitro { get; set; }
        [StringLength(35, ErrorMessage = "El nombre del árbitro es muy extenso")]
        public string Nombre { get; set; }
        public List<ArbitroPorPartidoVM> ArbitroPorPartidoes { get; set; }
        public List<CategoriaVM> Categorias { get; set; }
    }
}