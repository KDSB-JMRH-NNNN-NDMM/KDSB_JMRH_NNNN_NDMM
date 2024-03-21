using static KDSB_JMRH_NNNN_NDMM.Models.Suppliers;

namespace KDSB_JMRH_NNNN_NDMM.Models
{
    public class PhoneNumbers
    {

        public class PhoneNumber
        {
            public int Id { get; set; }
            public int SupplierId { get; set; }
            public string PhoneNumbers { get; set; }
            public string Note { get; set; }
            public Suppliers Supplier { get; set; } // Navigation property
        }

    }
}
