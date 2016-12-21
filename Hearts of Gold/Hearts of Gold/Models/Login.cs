//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Hearts_of_Gold.Models
{
    using System;
    using System.Collections.Generic;
    
    [Bind(Exclude = "ID")]
    public partial class Login
    {
        public Login()
        {
            this.Users = new HashSet<User>();
        }

        [ScaffoldColumn(false)]
        public int ID { get; set; }
        
        [Required]
        [DisplayName("User Name")]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    
        public virtual ICollection<User> Users { get; set; }
    }
}
