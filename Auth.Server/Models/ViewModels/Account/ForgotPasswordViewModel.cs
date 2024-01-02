using System.ComponentModel.DataAnnotations;

namespace Auth.Server.Models.ViewModels.Account
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }

}
