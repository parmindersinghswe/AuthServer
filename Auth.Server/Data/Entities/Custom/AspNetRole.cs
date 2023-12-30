using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using Auth.Server.Data.Entities.General;

namespace Auth.Server.Data.Entities.Custom
{
    public class AspNetRole : IdentityRole<int>
    {
        public ICollection<AspNetUserRole> UserRoles { get; set; }
        public ICollection<IdentityRoleClaim<int>> RoleClaims { get; set; }
        [NotMapped]
        public ICollection<Member> Members { get; set; }
        [NotMapped]
        public ICollection<AspnetUser> Users { get; set; }

    }
}
