

using System.ComponentModel.DataAnnotations;

namespace KDSB_JMRH_NNNN_NDMM.Models
{
    public class PhoneNumbers
    {

        
        
            public int Id { get; set; }
            public int SupplierId { get; set; }
        [Display(Name = "Número teléfonico:")]
        
        public string PhoneNumber { get; set; }
        [Display(Name = "Tipo de proveedor:")]
        
        public string Note { get; set; }
            public Suppliers Supplier { get; set; } // Navigation property
        

    }
}
