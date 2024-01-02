using System.Net;
using System.Text;
using System.Text.Json;
using Auth.Domain.Utilities;
using Auth.Domain.Attributes;
using Auth.Domain.Modals.Auth;
using Microsoft.AspNetCore.Http;
using Auth.Domain.Modals.Configurations;
using Microsoft.Extensions.Configuration;

namespace Auth.Domain.Middlewares
{
	public class AuthorizationMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly IConfiguration _configuration;
		public AuthorizationMiddleware(RequestDelegate next, IConfiguration configuration)
		{
			_next = next;
			_configuration = configuration;
			
		}

		public async Task InvokeAsync(HttpContext context)
		{
			var authAttribute = context.GetEndpoint()?.Metadata?.GetMetadata<AuthorizeAttribute>();

			if (authAttribute != null)
			{
				HttpResponseMessage validationResponse = null;
				var appSettings = ConfigurationUtility.GetConfigurationSection<Appsettings>("AuthServer", _configuration);
				var authCallerApi = appSettings != null && appSettings.AuthorizationSettings != null ? UrlUtility.CoalesceUrls(appSettings.AuthorizationSettings.GatewayUrl, appSettings.AuthorizationSettings.AuthServerUrl) : null;
				var callerApi = $"{context.Request.Scheme}://{context.Request.Host}";
				var domainUrl = UrlUtility.CoalesceUrls(authCallerApi, callerApi);
				if (domainUrl != null)
				{

					var permission = authAttribute.Policy;
					var authHeader = context.Request.Headers["Authorization"].FirstOrDefault();
					if (authHeader == null || !authHeader.StartsWith("Bearer "))
					{
						context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
						return;
					}
					var token = authHeader.Substring("Bearer ".Length);
					var validateModal = new ValidateModal { Token = token, ClaimType = permission };
					var postBody = JsonSerializer.Serialize(validateModal);
					var postData = new StringContent(postBody, Encoding.UTF8, "application/json");
					var authApi = UrlUtility.CombineUrls(domainUrl, "/Auth/Validate");
					var httpMessage = new HttpRequestMessage(HttpMethod.Post, authApi)
					{
						Content = postData,
					};
					using (var httpClient = new HttpClient())
					{
						validationResponse = await httpClient.SendAsync(httpMessage);
					}
				}
				if (validationResponse == null || !validationResponse.IsSuccessStatusCode)
				{
					context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
					return;
				}

				var userId = validationResponse.Content.ReadAsStringAsync().Result;
				context.Items["userId"] = userId;
			}

			await _next(context);
		}
	}
}

