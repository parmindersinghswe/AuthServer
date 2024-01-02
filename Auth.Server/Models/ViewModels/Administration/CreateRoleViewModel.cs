using System.ComponentModel.DataAnnotations;

namespace Auth.Server.Models.ViewModels.Administration
{
    public class CreateRoleViewModel
    {
        [Required]
        [Display(Name = "Role")]
        public string RoleName { get; set; }
    }
}
