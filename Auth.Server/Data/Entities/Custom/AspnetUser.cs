using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using Auth.Server.Data.Entities.General;

namespace Auth.Server.Data.Entities.Custom
{
    //[Table("AspNetUsers", Schema = "dbo")]
    public class AspnetUser : IdentityUser<int>
    {
        public Member Member { get; set; }
        public ICollection<AspNetUserRole> UserRoles { get; set; }
        [NotMapped]
        public ICollection<string> Roles { get; set; }
    }
}
