using Microsoft.AspNetCore.Http;
using System.Text;

namespace Auth.Domain.Middlewares
{
	public class RegisterClaims
	{
		private readonly RequestDelegate _next;
		private readonly HttpClient _httpClient;
		public static bool FirstRender = true;
		public RegisterClaims(RequestDelegate next, HttpClient httpClient)
		{
			_next = next;
			_httpClient = httpClient;
		}

		public async Task InvokeAsync(HttpContext context)
		{
			if (FirstRender)
			{
				FirstRender = false;
				try
				{
					Thread.Sleep(500);
					var postData = new StringContent(string.Empty, Encoding.UTF8, "application/json");
					using (var httpClient = new HttpClient())
					{
						var httpMessage = new HttpRequestMessage(HttpMethod.Post, "http://localhost:5257/MicroServiceConfigurations/CreatePolicy")
						{
							Content = postData
						};
						var response = await _httpClient.SendAsync(httpMessage);

					}
				}
				catch (Exception ex)
				{
					FirstRender = true;
				}
			}
			
			await _next(context);
		}
	}
}
