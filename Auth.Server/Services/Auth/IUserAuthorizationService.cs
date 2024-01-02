using Auth.Server.Data.Entities.Custom;
using Auth.Server.Models.Authorization;
using Auth.Server.ViewModels;

namespace Auth.Server.Services.Auth
{
    public interface IUserAuthorizationService
    {
        Task<object> Authenticate(AuthenticateRequest model);
        Task<object> Create(RegistrationViewModel model, bool? isAdmin = false);
        Task<object> ConfirmEmail(string userId, string token);
        Task AddClaims(List<string> claims);
        Task AddClaimToRole(int roleId, string claimType, string claimValue);
        AspnetUser ValidateClaims(int userId, string policy);
    }
}
