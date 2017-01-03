﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hearts_of_Gold.Models
{
    //public class Metadata
    //{
    //}

    public class ItemMetadata
    {
        public int ItemID { get; set; }

        [DisplayName("Category")]
        public int CategoryID { get; set; }

        [DisplayName("Location")]
        public int LocationID { get; set; }
        public int UserID { get; set; }

        [DisplayName("Item")]
        public string Item1 { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; }
    }

    public class LocationMetadata
    {
        [DisplayName("Business Name")]
        public string BusinessName { get; set; }
    }

    public class UserMetadata
    {
        public int UserID { get; set; }
        public string AspNetUsersId { get; set; }

        [DisplayName("First Name")]
        [Required(ErrorMessage = "You must enter a First Name")]
        public string Firstname { get; set; }

        [DisplayName("Last Name")]
        [Required(ErrorMessage = "You must enter a Last Name")]
        public string Lastname { get; set; }

        [DisplayName("Street Address")]
        [Required(ErrorMessage = "You must enter a Street Address")]
        public string Streetaddress { get; set; }

        [DisplayName("Date of Birth")]
        public System.DateTime Date_of_Birth { get; set; }
        
        [DisplayName(" ")]
        [HiddenInput(DisplayValue = false)]
        public Nullable<bool> IsDeleted { get; set; }
    }
}