using System.ComponentModel.DataAnnotations;

namespace Auth.Server.Models.Authorization
{
    public class AuthenticateRequest
    {
        [Required]
		public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
