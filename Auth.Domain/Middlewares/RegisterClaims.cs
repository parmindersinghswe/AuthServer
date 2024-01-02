using System.Text;
using Auth.Domain.Utilities;
using Microsoft.AspNetCore.Http;
using Auth.Domain.Modals.Configurations;
using Microsoft.Extensions.Configuration;

namespace Auth.Domain.Middlewares
{
	public class RegisterClaims
	{
		private readonly RequestDelegate _next;
		public static bool FirstRender = true;
		private readonly IConfiguration _configuration;
		private  static int _counter = 0;
        public RegisterClaims(RequestDelegate next, IConfiguration configuration)
		{
			_next = next;
			_configuration = configuration; 
		}

		public async Task InvokeAsync(HttpContext context)
		{
			if (_counter++>=2 && FirstRender)
			{
				FirstRender = false;
				try
				{
					var appSettings = ConfigurationUtility.GetConfigurationSection<Appsettings>("AuthServer", _configuration);
					if (appSettings != null && appSettings.Permissions != null && !string.IsNullOrEmpty(appSettings.Permissions.CreatePolicyLocalApi))
					{
						var postData = new StringContent(string.Empty, Encoding.UTF8, "application/json");
						var httpMessage = new HttpRequestMessage(HttpMethod.Post, appSettings.Permissions.CreatePolicyLocalApi)
						{
							Content = postData
						};
						HttpResponseMessage? response = null;
						using (var httpClient = new HttpClient())
						{
							response = await httpClient.SendAsync(httpMessage);
							
						}
						if (response == null || !response.IsSuccessStatusCode)
						{
							return;
						}
					}
				}
				catch
				{
					FirstRender = true;
					return;
				}
			}
			
			await _next(context);
		}
	}
}
