using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcIntro0.Models
{
    public class Account : IdentityUser
    {
        public IdentityRole Role { get; set; }
    }
}
