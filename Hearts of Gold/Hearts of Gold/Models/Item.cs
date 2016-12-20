//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Hearts_of_Gold.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Item
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Item()
        {
            this.Requests = new HashSet<Request>();
        }
    
        public int ItemID { get; set; }
        public int CategoryID { get; set; }
        public int LocationID { get; set; }
        public int UserID { get; set; }
        public string Item1 { get; set; }
        public int Quantity { get; set; }
    
        public virtual Donation_Categories Donation_Categories { get; set; }
        public virtual Donation_Location Donation_Location { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Request> Requests { get; set; }
        public virtual User User { get; set; }
    }
}
