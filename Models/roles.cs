using System.ComponentModel.DataAnnotations;

namespace KDSB_JMRH_NNNN_NDMM.Models
{
    public class roles
    {
            public int Id { get; set; }

        [Display (Name = "Nombre:")]
        [StringLength(25, ErrorMessage = "El campo debe tener como máximo 25 caracteres.")]
        public string Name { get; set; }
        [Display(Name = "Descripción:")]
        [StringLength(50, ErrorMessage = "El campo debe tener como máximo 25 caracteres.")]
        public string Description { get; set; }
  
    }
}
