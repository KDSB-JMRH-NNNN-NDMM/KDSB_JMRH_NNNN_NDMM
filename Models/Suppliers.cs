namespace KDSB_JMRH_NNNN_NDMM.Models
{
    public class Suppliers
    {
        public Suppliers()
        {
            PhoneNumber = new List<PhoneNumbers>();
        }
        
            public int Id { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }
            public string Address { get; set; }
            public IList<PhoneNumbers> PhoneNumber { get; set; } // Navigation property
        

    }
}
