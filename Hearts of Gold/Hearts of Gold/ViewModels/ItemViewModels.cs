using System.ComponentModel;
using Hearts_of_Gold.Models;

namespace Hearts_of_Gold.ViewModels
{
    public class ItemViewModels
    {
        public int ItemID { get; set; }
        //public int CategoryID { get; set; }
        //public int LocationID { get; set; }
        //private int UserID { get; set; }
        public string AspNetUsersId { get; set; }
        public string Item { get; set; }

        [DisplayName("Item Quantity")]
        public int Quantity { get; set; }
        public string Description { get; set; }
        

        public virtual Donation_Categories Donation_Categories { get; set; }
        public virtual Donation_Location Donation_Location { get; set; }

        public static implicit operator ItemViewModels(Item item)
        {
            return new ItemViewModels
            {
                ItemID = item.ItemID,
                //CategoryID = item.CategoryID,
                //LocationID = item.LocationID,
                //UserID = item.UserID,
                Item = item.Item1,
                Quantity = item.Quantity,
                Description = item.Description,
                Donation_Categories = item.Donation_Categories,
                Donation_Location = item.Donation_Location

            };
        }

        public static implicit operator Item(ItemViewModels vm)
        {
            return new Item
            {
                ItemID = vm.ItemID,
                //CategoryID = vm.CategoryID,
                //LocationID = vm.LocationID,
                //UserID = vm.UserID,
                Item1 = vm.Item,
                Quantity = vm.Quantity,
                Description = vm.Description,
                Donation_Categories = vm.Donation_Categories,
                Donation_Location = vm.Donation_Location
            };
        }
    } // End of Item View Model Class
}