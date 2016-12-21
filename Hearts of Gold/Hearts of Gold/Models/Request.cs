//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Hearts_of_Gold.Models
{
    using System;
    using System.Collections.Generic;
    [Bind(Exclude = "RequestID")]
    public partial class Request
    {
        [ScaffoldColumn(false)]
        public int RequestID { get; set; }

        [ScaffoldColumn(false)]
        public int DonationItemID { get; set; }

        [ScaffoldColumn(false)]
        public int LocationID { get; set; }

        [ScaffoldColumn(false)]
        public int RequesterID { get; set; }

        [Required]
        [Range(0,1000,ErrorMessage = "Quantity must be between 0 and 1000")]
        public int Qauntity { get; set; }
    
        public virtual Donation_Location Donation_Location { get; set; }
        public virtual Item Item { get; set; }
        public virtual User User { get; set; }
    }
}
