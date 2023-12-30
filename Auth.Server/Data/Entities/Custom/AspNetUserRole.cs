using Microsoft.AspNetCore.Identity;

namespace Auth.Server.Data.Entities.Custom
{
    public class AspNetUserRole : IdentityUserRole<int>
    {
        public AspnetUser User { get; set; }
        public AspNetRole Role { get; set; }
    }
}
