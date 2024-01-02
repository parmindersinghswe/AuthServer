using System.ComponentModel.DataAnnotations;
using Auth.Server.Data.Interfaces;

namespace Auth.Server.Data.Entities.General.Parent
{
    public class MemberModel : IEntity<int>
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        //  [Required, EmailAddress]
        //public string Email { get; set; }

    }
}
