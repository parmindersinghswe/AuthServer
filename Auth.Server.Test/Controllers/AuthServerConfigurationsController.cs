using Auth.Domain.Attributes;
using Auth.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace Auth.Server.Test.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class AuthServerConfigurationsController : ControllerBase
	{
		private readonly ClaimsService _ClaimsService;
		public AuthServerConfigurationsController(ClaimsService claimsService)
        {
			_ClaimsService = claimsService;

		}
        [HttpPost]
		[Route("CreatePolicy")]
		public IActionResult CreatePolicy()
		{
			var assembly = Assembly.GetExecutingAssembly(); // or use Assembly.Load("Your.Assembly.Name")
			_ClaimsService.RegisterClaims(assembly);
			return Ok();
		}

	}
}
