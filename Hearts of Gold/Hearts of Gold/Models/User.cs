//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;

namespace Hearts_of_Gold.Models
{
    using System;
    using System.Collections.Generic;

    [MetadataType(typeof(UserMetadata))]
    public partial class User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            this.Items = new HashSet<Item>();
            this.Requests = new HashSet<Request>();
        }
    
        public int UserID { get; set; }
        public string AspNetUsersId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Streetaddress { get; set; }
        public System.DateTime Date_of_Birth { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
    
        public virtual AspNetUser AspNetUser { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Item> Items { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Request> Requests { get; set; }
    }
}
