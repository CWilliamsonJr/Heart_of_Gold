using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hearts_of_Gold.Models
{
    public class FooWithLocationViewModel
    {
        public IEnumerable<FooWithLocationViewModel> Foos { get; set; }

        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}