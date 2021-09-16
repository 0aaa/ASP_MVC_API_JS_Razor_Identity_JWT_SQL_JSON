using Microsoft.AspNetCore.Identity;

namespace MvcIntro0.Models
{
    public class Account : IdentityUser
    {
        public IdentityRole Role { get; set; }
    }
}
