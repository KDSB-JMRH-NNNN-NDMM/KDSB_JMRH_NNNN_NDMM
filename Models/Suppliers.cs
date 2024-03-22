using System.ComponentModel.DataAnnotations;

namespace KDSB_JMRH_NNNN_NDMM.Models
{
    public class Suppliers
    {
        public Suppliers()
        {
            PhoneNumber = new List<PhoneNumbers>();
        }
        
            public int Id { get; set; }
        [Display(Name = "Nombre:")]
        
        public string Name { get; set; }
        [Display(Name = "Correo electronico:")]
        
        public string Email { get; set; }
        [Display(Name = "Dirección:")]
        
        public string Address { get; set; }
        [Display(Name = "Número teléfonico:")]
        public IList<PhoneNumbers> PhoneNumber { get; set; } // Navigation property
        

    }
}
