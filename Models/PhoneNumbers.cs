

using System.ComponentModel.DataAnnotations;

namespace KDSB_JMRH_NNNN_NDMM.Models
{
    public class PhoneNumbers
    {

        
        
            public int Id { get; set; }
            public int SupplierId { get; set; }
        [Display(Name = "Número teléfonico:")]
        [StringLength(15, ErrorMessage = "El campo debe tener como máximo 25 caracteres.")]
        public string PhoneNumber { get; set; }
        [Display(Name = "Tipo de proveedor:")]
        [StringLength(20, ErrorMessage = "El campo debe tener como máximo 25 caracteres.")]
        public string Note { get; set; }
            public Suppliers Supplier { get; set; } // Navigation property
        

    }
}
