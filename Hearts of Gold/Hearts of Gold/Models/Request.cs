namespace Hearts_of_Gold.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Request
    {
        public int RequestID { get; set; }

        public int DonationItemID { get; set; }

        public int LocationID { get; set; }

        public int RequesterID { get; set; }

        public int Quantity { get; set; }

        public bool ItemPickedUp { get; set; }

        public virtual Donation_Location Donation_Location { get; set; }

        public virtual Item Item { get; set; }

        public virtual User User { get; set; }
    }
}
