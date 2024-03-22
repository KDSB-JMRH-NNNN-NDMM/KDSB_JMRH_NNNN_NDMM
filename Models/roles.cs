using System.ComponentModel.DataAnnotations;

namespace KDSB_JMRH_NNNN_NDMM.Models
{
    public class roles
    {
            public int Id { get; set; }

        [Display (Name = "Nombre:")]    
            public string Name { get; set; }
        [Display(Name = "Descripción:")]
        public string Description { get; set; }
  
    }
}
