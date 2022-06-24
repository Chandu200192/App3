using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App3.Utilities
{
    public class ApplicationUser:IdentityUser
    {
        public string city { get; set; }
    }
}
