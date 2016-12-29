using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Hearts_of_Gold.App_Start
{
    public class MyDbContext : IdentityDbContext<AppUser>
    {
    }
}