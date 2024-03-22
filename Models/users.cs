using System.ComponentModel.DataAnnotations;
using static KDSB_JMRH_NNNN_NDMM.Models.roles;

namespace KDSB_JMRH_NNNN_NDMM.Models
{
    public class users
    {
            public int Id { get; set; }
        [Display(Name = "Nombre de usuario:")]
       
        public string UserName { get; set; }
        [Display(Name = "Contraseña:")]
        public string Password { get; set; }
        [Display(Name = "Correo electronico:")]
        
        public string Email { get; set; }
        [Display(Name = "Estado:")]
        
        public int Status { get; set; }
        [Display(Name = "Imagen:")]
        public byte[] Image { get; set; }
        [Display(Name = "Rol:")]
        public int RoleId { get; set; }
        [Display(Name = "Rol:")]
        public roles Role { get; set; } // Navigation property
    }
}
