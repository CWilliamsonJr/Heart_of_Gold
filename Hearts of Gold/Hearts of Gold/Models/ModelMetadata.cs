using System;
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
    //[ScaffoldTable(true)]
    public class ItemMetadata
    {
        [DisplayName("Category")]
        public int CategoryID { get; set; }

        [DisplayName("Location")]
        public int LocationID { get; set; }

        [DisplayName("Item")]
        [Required(ErrorMessage = "Must provide item name")]
        public string Item1 { get; set; }


        [Range(1, int.MaxValue, ErrorMessage = "Must enter a value greater than 0")]
        public int Quantity { get; set; }
        public string Description { get; set; }
    }

    public class RequestMetadata
    {
        [DisplayName("Donated Item")]
        public int DonationItemID { get; set; }

        [DisplayName("Item's Location")]
        public int LocationID { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int RequesterID { get; set; }

        [DisplayName("Item Quanity")]
        [Range(1, int.MaxValue, ErrorMessage = "Must enter a value greater than 0")]
        public int Quantity { get; set; }

        [DisplayName("Received Item")]
        public bool ItemPickedUp { get; set; }
    }

    public class LocationMetadata
    {
        [DisplayName("Business Name")]
        public string BusinessName { get; set; }

        [DisplayName("Location")]
        public int LocationID { get; set; }
    }

    public class CategoryMetadata
    {
        [DisplayName("Category")]
        public int CategoryID { get; set; }

        [DisplayName("Item Category")]
        public string Categories { get; set; }
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
        [Required(ErrorMessage = "You must enter a Date of Birth")]
        public System.DateTime Date_of_Birth { get; set; }

        [DisplayName(" ")]
        [HiddenInput(DisplayValue = false)]
        public bool IsDeleted { get; set; }
    }
}