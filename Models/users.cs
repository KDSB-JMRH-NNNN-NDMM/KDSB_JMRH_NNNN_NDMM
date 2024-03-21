using static KDSB_JMRH_NNNN_NDMM.Models.roles;

namespace KDSB_JMRH_NNNN_NDMM.Models
{
    public class users
    {
        public class User
        {
            public int Id { get; set; }
            public string UserName { get; set; }
            public string Password { get; set; }
            public string Email { get; set; }
            public int Status { get; set; }
            public byte[] Image { get; set; }
            public int RoleId { get; set; }
            public Role Role { get; set; } // Navigation property
        }

    }
}
