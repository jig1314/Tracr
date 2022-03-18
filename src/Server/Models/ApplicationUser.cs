using Microsoft.AspNetCore.Identity;

namespace Tracr.Server.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUserDetail ApplicationUserDetail { get; set; }

        public List<Property> Properties { get; set; }
    }
}