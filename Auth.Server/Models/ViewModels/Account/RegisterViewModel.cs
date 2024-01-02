using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Auth.Server.Models.ViewModels.Account
{
    public class RegisterViewModel
    {
        [Required]
        public string FirstName { get; set; }

        public string MiddleName { get; set; }
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        [Remote(action: "IsEmailInUse", controller: "Account")]
        public string Email { get; set; }

        public string MobileNumber { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        // [DataType(DataType.Password)]
        // [Display(Name = "Confirm password")]
        // [Compare("Password",
        //     ErrorMessage = "Password and confirmation password do not match.")]
        // public string ConfirmPassword { get; set; }
    }
    public class RegisterResultViewModel
    {
        public bool IsRegistered { get; set; }
        public string UserName { get; set; }

        public string Error { get; set; }
        public string EmailConfirmationLink { get; set; }
    }
}
