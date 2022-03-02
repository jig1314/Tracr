namespace Tracr.Server.Models
{
    public class ApplicationUserDetail
    {
        public string ApplicationUserId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
