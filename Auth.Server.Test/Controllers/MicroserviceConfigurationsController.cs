using Auth.Domain.Attributes;
using Auth.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace Auth.Server.Test.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class MicroserviceConfigurationsController : ControllerBase
	{
		[Route("CallMe")]
		[HttpGet]
		public IActionResult CallMe()
		{
			return Ok("Everything is ok");
		}
		[HttpPost]
		[Route("CreatePolicy")]
		public IActionResult CreatePolicy()
		{
			var assembly = Assembly.GetExecutingAssembly(); // or use Assembly.Load("Your.Assembly.Name")

			var classesWithAttribute = assembly.GetTypes()
				.Where(type => type.GetCustomAttributes(typeof(AuthorizeAttribute), false).Any())
				.ToList();
			var membersWithAttribute = assembly.GetTypes()
	.SelectMany(type => type.GetMembers())
	.Where(member => member.GetCustomAttributes(typeof(AuthorizeAttribute), false).Any())
	.ToList();
			var attributeParameterValues = classesWithAttribute
				.Select(type =>
					((AuthorizeAttribute)type.GetCustomAttributes(typeof(AuthorizeAttribute), false).First()).Policy)
				.ToList();
			attributeParameterValues.AddRange(membersWithAttribute.Select(m => ((AuthorizeAttribute)m.GetCustomAttributes(typeof(AuthorizeAttribute), false).First()).Policy).ToList());
			ClaimsService.RegisterClaims(attributeParameterValues);
			return Ok();
		}

	}
}
