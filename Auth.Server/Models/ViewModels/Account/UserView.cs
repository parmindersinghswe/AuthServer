using Microsoft.AspNetCore.Identity;

namespace Auth.Server.Models.ViewModels.Account
{
    public class UserView
    {
        public IdentityUser User { get; set; }
        public List<UserRole> Roles { get; set; }
    }

    public class UserRole
    {
        public IdentityRole Role { get; set; }
        public bool IsUserRole { get; set; }
    }
}
