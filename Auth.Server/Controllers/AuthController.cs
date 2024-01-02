using Auth.Domain.Attributes;
using Auth.Domain.Modals.Auth;
using Auth.Server.Data.Entities.Custom;
using Auth.Server.Models.Authorization;
using Auth.Server.Services.Auth;
using Auth.Server.Utilities;
using Auth.Server.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Server.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class AuthController : ControllerBase
    {
        //private readonly IConfiguration _configuration;

        private readonly RoleManager<AspNetRole> _roleManager;
        private readonly IJwtUtils _jwtUtils;
        private readonly IUserAuthorizationService _userAuthorizationService;
        public AuthController(IJwtUtils jwtUtils, IUserAuthorizationService userAuthorizationService, RoleManager<AspNetRole> roleManager)
        {
            _jwtUtils = jwtUtils;
            _userAuthorizationService = userAuthorizationService;
            _roleManager = roleManager;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] AuthenticateRequest model)
        {
            var response = await _userAuthorizationService.Authenticate(model);
            if (response != null)
            {
                return Ok(response);
            }
            return BadRequest("Username or Password are not correct");
        }
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegistrationViewModel model)
        {
            var response = await _userAuthorizationService.Create(model);

            if (response != null)
            { return Ok(response); }
            return BadRequest("something went wrong");
        }
        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            var response = await _userAuthorizationService.ConfirmEmail(userId, token);
            if (response != null)
            { return Ok(response); }

            // Store user data in AspNetUsers database table

            return BadRequest("something went wrong");
        }

        [HttpPost]
        public IActionResult Validate([FromBody] ValidateModal validateModal)
        {
            var userId = _jwtUtils.ValidateJwtToken(validateModal.Token);
            if (userId.HasValue)
            {
                var user = _userAuthorizationService.ValidateClaims(userId.Value, validateModal.ClaimType);
                return user != null ? Ok(new { UserId = user.Id }) : Unauthorized("Not valid");
            }
            return Unauthorized("Invalid User");
        }
        [HttpPost]
        [Authorize("Claims")]
        public async Task<IActionResult> AddClaims(List<string> claims)
        {
            if (claims != null && claims.Any())
            {
                await _userAuthorizationService.AddClaims(claims);
                //foreach (var claim in claims)
                //{
                //    await AddClaimToRole(1, claim, claim);
                //}
                return Ok();
            }
            return BadRequest();
        }
        [HttpPost]
        [Authorize("Claims")]
        public async Task<IActionResult> AddClaimToRole(int roleId, string claimType, string claimValue)
        {
            try
            {
                await _userAuthorizationService.AddClaimToRole(roleId, claimType, claimValue);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return Ok();
        }

    }
}
