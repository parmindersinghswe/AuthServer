using System.Web;
using System.Text;
using System.Security.Claims;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Auth.Server.Data.Entities.Custom;
using Auth.Server.Models.Authorization;
using Auth.Server.Models.MailModels;
using Auth.Server.Helpers;
using static Auth.Server.DataModels.DateModelConstants;
using Auth.Server.ViewModels;
using Auth.Server.Services.Utility;
using Auth.Server.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Auth.Server.Services.Auth
{
    public class UserAuthorizationService : IUserAuthorizationService
    {
        private readonly UserManager<AspnetUser> userManager;
        private readonly SignInManager<AspnetUser> signInManager;
        private readonly IMailService _mailService;
        private readonly AppSettings _appSettings;
        private readonly ILogger<UserAuthorizationService> _logger;
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly RoleManager<AspNetRole> _roleManager;

        public UserAuthorizationService(IOptions<AppSettings> appSettings, UserManager<AspnetUser> _userManager, SignInManager<AspnetUser> _signInManager, IMailService mailservice, ILogger<UserAuthorizationService> logger, IApplicationDbContext applicationDbContext, RoleManager<AspNetRole> roleManager)
        {
            _appSettings = appSettings.Value;
            userManager = _userManager;
            signInManager = _signInManager;
            _mailService = mailservice;
            _logger = logger;
            _applicationDbContext = applicationDbContext;
            _roleManager = roleManager;
        }
        public async Task AddClaimToRole(int roleId, string claimType, string claimValue)
        {
            var claim = new Claim(claimType, claimValue);
            var role = _roleManager.Roles.Include(x => x.RoleClaims).Include(x=> x.UserRoles).ThenInclude(y=> y.User).FirstOrDefault(x => x.Id == roleId);
            
            if (role != null)
            {
                var hasClaim = role.RoleClaims.Any(c => c.ClaimType == claimType && c.ClaimValue == claimValue);
                if (!hasClaim)
                {
                    await _roleManager.AddClaimAsync(role, claim);
                }
            }
        }
        public async Task AddClaims(List<string> claims)
        {
            foreach (var claim in claims) {
                if (_applicationDbContext.AspNetClaims.FirstOrDefault(x => x.ClaimType == claim && x.ClaimValue == claim) == null)
                {
                    var added = await _applicationDbContext.AspNetClaims.AddAsync(new AspNetClaim()
                    {
                        ClaimType = claim,
                        ClaimValue = claim
                    });
                }
            }
            _applicationDbContext.SaveChanges();

        }
        public async Task<object> Authenticate(AuthenticateRequest model)
        {

            try
            {
                var user = await userManager.FindByNameAsync(model.Username);

                if (AuthorizationConfiguration.RequireConfirmedEmail && user != null && !user.EmailConfirmed && await userManager.CheckPasswordAsync(user, model.Password))
                {
                    return new { ErrorMessage = "Email not confirmed yet" };
                }

                var result = await signInManager.PasswordSignInAsync(
                    model.Username, model.Password, false, false);
                if (result.Succeeded)
                {
                    var token = GenerateJwtToken(user);
                    return new AuthenticateResponse(user, token);
                }
            }
            catch (Exception e)
            {
                _logger.Log(LogLevel.Error, e.Message);
            }
            return null;
        }
        public async Task<object> Create(RegistrationViewModel model, bool? isAdmin = false)
        {
            var createWithAdmin = isAdmin.HasValue && isAdmin.Value;
            var user = new AspnetUser
            {
                UserName = model.Email,
                Email = model.Email
            };
            try
            {
                var result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, isPersistent: false);
                    user = await userManager.FindByEmailAsync(model.Email);
                    _ = userManager.AddToRoleAsync(user, Role.User.ToString());
                    if (createWithAdmin)
                    {
                        _ = userManager.AddToRoleAsync(user, Role.Admin.ToString());
                        return GetAuthenticatedResponse(user);
                    }
                    if (AuthorizationConfiguration.RequireConfirmedEmail)
                    {
                        var emailConfirmationToken = await userManager.GenerateEmailConfirmationTokenAsync(user);
                        if (createWithAdmin)
                        {
                            _ = userManager.ConfirmEmailAsync(user, emailConfirmationToken);
                            return GetAuthenticatedResponse(user);
                        }
                        var encodedProperties = $"userId={HttpUtility.UrlEncode(user.Id.ToString())}&token={HttpUtility.UrlEncode(emailConfirmationToken)}";
                        var emailConfirmationUrl = $"{AuthorizationConfiguration.HostUrl}?{encodedProperties}";
                        var encodedBody = $"Please <a href='{emailConfirmationUrl}'>click here</a> the confirmation of your email address to continue to the Online Punjab Services.";
                        var mailRequest = new MailRequest()
                        {
                            ToEmail = user.Email,
                            Subject = "Online Punjab Email Confirmation",
                            Body = encodedBody
                        };
                        await _mailService.SendEmailAsync(mailRequest);
                        return new { AccountCreated = true, EmailConfirmed = false };
                    }
                    else
                    {
                        return GetAuthenticatedResponse(user);
                    }
                }
            }
            catch (Exception e)
            {
                var message = e.Message;
            }
            return null;
        }

        public async Task<object> ConfirmEmail(string userId, string token)
        {
            if (userId == null || token == null)
            {
                return null;
            }

            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return new { ErrorMessage = "Invalid User Id" };
            }
            var result = await userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                return new { IsConfirmed = true };
            }

            return new { isConfirmed = false };
        }
        private AuthenticateResponse GetAuthenticatedResponse(AspnetUser user)
        {
            var jwtToken = GenerateJwtToken(user);
            return new AuthenticateResponse(user, jwtToken);
        }
        private string GenerateJwtToken(AspnetUser user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public AspnetUser ValidateClaims(int userId, string policy)
        {
            var user = userManager.Users.Include(x=> x.UserRoles).ThenInclude(x=> x.Role).ThenInclude(x=> x.RoleClaims).FirstOrDefault(x=> x.Id == userId);
            if(user != null)
            {
                var roleClaims = user.UserRoles.SelectMany(x => x.Role.RoleClaims).Where(x=> x.ClaimType == policy);
                if(!roleClaims.Any())
                {
                    user = null;
                }
            }
            return user;
        }
    }

}
