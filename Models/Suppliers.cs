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
        [Required(ErrorMessage = "El Nombre es obligatorio")]
        public string Name { get; set; }
        [Display(Name = "Correo electronico:")]
        [Required(ErrorMessage = "El Correo electronico es obligatorio")]

        public string Email { get; set; }
        [Display(Name = "Dirección:")]
        [Required(ErrorMessage = "La Dirección es obligatorio")]

        public string Address { get; set; }
        [Display(Name = "Número teléfonico:")]
        [Required(ErrorMessage = "El Número teléfonico es obligatorio")]

        public IList<PhoneNumbers> PhoneNumber { get; set; } // Navigation property
        

    }
}
