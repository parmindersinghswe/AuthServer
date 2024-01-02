using Auth.Domain.Attributes;
using Auth.Domain.Modals.Configurations;
using Auth.Domain.Utilities;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.Json;

namespace Auth.Domain.Services
{
	public class AuthenticateResponse
	{
		public int? id { get; set; }
		public string? username { get; set; }
		public string? email { get; set; }
		public string? token { get; set; }
	}
	public class ClaimsService
	{
		private readonly IConfiguration _configuration;
		public ClaimsService(IConfiguration configuration)
        {
			_configuration = configuration;   
        }
        public static HttpClient HttpClient { get; set; } = new HttpClient();

		public void RegisterClaims(Assembly assembly )
		{
			if ( assembly != null )
			{
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
				RegisterClaims(attributeParameterValues);
			}
		}
		public async void RegisterClaims(List<string> claims)
		{
			var user = new { Username = "admin@gmail.com", Password = "123456" };
			string userBody = JsonSerializer.Serialize(user);
			var postContent = new StringContent(userBody, Encoding.UTF8, "application/json");

			string postBody = JsonSerializer.Serialize(claims);
			ServicePointManager.Expect100Continue = true;
			// ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls | SecurityProtocolType.Ssl3;
			var postData = new StringContent(postBody, Encoding.UTF8, "application/json");
			var appSettings = ConfigurationUtility.GetConfigurationSection<Appsettings>("AuthServer", _configuration);
			var domainUrl = appSettings != null && appSettings.AuthorizationSettings != null ? UrlUtility.CoalesceUrls(appSettings.AuthorizationSettings.GatewayUrl, appSettings.AuthorizationSettings.AuthServerUrl) : null;
			if (!string.IsNullOrEmpty(domainUrl))
			{
				using (var httpClient = new HttpClient())
				{
					var authLoginApi = UrlUtility.CombineUrls(domainUrl, "/Auth/Login");
					var httpMessage = new HttpRequestMessage(HttpMethod.Post, authLoginApi)
					{
						Content = postContent
					};
					var response = await httpClient.SendAsync(httpMessage);
					if (response.IsSuccessStatusCode)
					{
						var result = response.Content.ReadAsStringAsync().Result;
						var responseData = JsonSerializer.Deserialize<AuthenticateResponse>(result);

						var addClaimsApi = UrlUtility.CombineUrls(domainUrl, "/Auth/AddClaims");
						httpMessage = new HttpRequestMessage(HttpMethod.Post, addClaimsApi)
						{
							Content = postData,
						};
						httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", responseData?.token);
						response = await httpClient.SendAsync(httpMessage);
						if (response.IsSuccessStatusCode)
						{

						}
						//var authHeader = response.Headers["Authorization"].FirstOrDefault();
						//if (authHeader == null || !authHeader.StartsWith("Bearer "))
						//{
						//    context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
						//    return;
						//}
						//var token = authHeader.Substring("Bearer ".Length);
					}
				}

			}
		}
	}
}
