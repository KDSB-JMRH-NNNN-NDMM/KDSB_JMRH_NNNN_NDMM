using System.ComponentModel.DataAnnotations;

namespace KDSB_JMRH_NNNN_NDMM.Models
{
    public class roles
    {
            public int Id { get; set; }

        [Display (Name = "Nombre:")]
        [Required(ErrorMessage = "El Nombre es obligatorio")]
        [StringLength(25, ErrorMessage = "El campo debe tener como máximo 25 caracteres.")]
        public string Name { get; set; }
        [Display(Name = "Descripción:")]
        [Required(ErrorMessage = "La descripción es obligatoria")]
        [StringLength(50, ErrorMessage = "El campo debe tener como máximo 25 caracteres.")]
        public string Description { get; set; }
  
    }
}
