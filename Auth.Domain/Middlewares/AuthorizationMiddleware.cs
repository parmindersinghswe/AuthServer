using Auth.Domain.Attributes;
using Auth.Domain.Modals.Auth;using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text;
using System.Text.Json;

namespace Auth.Domain.Middlewares
{
	public class AuthorizationMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly HttpClient _httpClient;

		public AuthorizationMiddleware(RequestDelegate next, HttpClient httpClient)
		{
			_next = next;
			_httpClient = httpClient;
		}

		public async Task InvokeAsync(HttpContext context)
		{
			var authAttribute = context.GetEndpoint()?.Metadata?.GetMetadata<AuthorizeAttribute>();

			if (authAttribute != null)
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
				var authApi = @"http://localhost:5124/Auth/Validate";//Can be Auth.Server Api OR Auth.Gateway Api
				var httpMessage = new HttpRequestMessage(HttpMethod.Post, authApi)
				{
					Content = postData,
				};
				var validationResponse = await _httpClient.SendAsync(httpMessage);
				// var validationResponse = await _httpClient.GetAsync($"Auth/Validate?token={token}&&permission={permission}");

				if (!validationResponse.IsSuccessStatusCode)
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

