using System.ComponentModel.DataAnnotations;
using static KDSB_JMRH_NNNN_NDMM.Models.roles;

namespace KDSB_JMRH_NNNN_NDMM.Models
{
    public class users
    {
            public int Id { get; set; }
        [Display(Name = "Nombre de usuario:")]
        [Required(ErrorMessage = "El Nombre de Usuario es obligatorio")]
        public string UserName { get; set; }
        [Display(Name = "Contraseña:")]
        [Required(ErrorMessage = "La Contraseña es obligatoria")]
        public string Password { get; set; }
        [Display(Name = "Correo electronico:")]
        [Required(ErrorMessage = "El Correo electronico es obligatorio")]
        public string Email { get; set; }
        [Display(Name = "Estado:")]
        [Required(ErrorMessage = "Digite su estado")]
        public int Status { get; set; }
        [Display(Name = "Imagen:")]
        public byte[] Image { get; set; }
        [Display(Name = "Rol:")]
        public int RoleId { get; set; }
        [Display(Name = "Rol:")]
        public roles Role { get; set; } // Navigation property
    }
}
