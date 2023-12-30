using Auth.Server.Data.Entities.Custom;

namespace Auth.Server.Models.Authorization
{
    public class AuthenticateResponse
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }


        public AuthenticateResponse(AspnetUser user, string token)
        {
            Id = user.Id;
            Username = user.UserName;
            Email = user.Email;
            Token = token;
        }
    }
}
