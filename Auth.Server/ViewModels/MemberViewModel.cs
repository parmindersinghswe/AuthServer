using System.ComponentModel.DataAnnotations;

namespace Auth.Server.ViewModels
{
    public class MemberViewModel
    {
        [Required]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
