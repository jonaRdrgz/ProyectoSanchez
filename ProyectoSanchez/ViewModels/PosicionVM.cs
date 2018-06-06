using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoSanchez.ViewModels
{
    public class PosicionVM
    {
        [Required(ErrorMessage = "El id de la posicion es obligatorio")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Sólo datos numéricos son permitidos")]
        public ushort IdPosicion { get; set; }

        [Required(ErrorMessage = "Es necesaria la descripción de la posición")]
        [StringLength(30,ErrorMessage = "Descripción de la posición muy extensa")]
        public string Descripcion { get; set; }

        public List<TitularEquipoPartidoVM> TitularEquipoPartidoes { get; set; }
    }
}