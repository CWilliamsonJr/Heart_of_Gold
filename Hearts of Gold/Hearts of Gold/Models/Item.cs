namespace Hearts_of_Gold.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [MetadataType(typeof(ItemMetadata))]
    public partial class Item
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Item()
        {
            Requests = new HashSet<Request>();
        }

        public int ItemID { get; set; }

        public int CategoryID { get; set; }

        public int LocationID { get; set; }

        public int UserID { get; set; }

        [Column("Item")]
        [Required]
        [StringLength(255)]
        public string Item1 { get; set; }

        public int Quantity { get; set; }

        [StringLength(255)]
        public string Description { get; set; }

        public virtual Donation_Categories Donation_Categories { get; set; }

        public virtual Donation_Location Donation_Location { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Request> Requests { get; set; }

        public virtual User User { get; set; }
    }
}
