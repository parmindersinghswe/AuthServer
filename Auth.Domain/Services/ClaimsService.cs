using System.Net;
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
	public static class ClaimsService
	{
		public static HttpClient HttpClient { get; set; } = new HttpClient();
		public static async void RegisterClaims(List<string> claims)
		{
			var user = new { Username = "admin@gmail.com", Password = "123456" };
			string userBody = JsonSerializer.Serialize(user);
			var postContent = new StringContent(userBody, Encoding.UTF8, "application/json");



			string postBody = JsonSerializer.Serialize(claims);
			ServicePointManager.Expect100Continue = true;
			// ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls | SecurityProtocolType.Ssl3;
			var postData = new StringContent(postBody, Encoding.UTF8, "application/json");

			using (var httpClient = new HttpClient())
			{
				var httpMessage = new HttpRequestMessage(HttpMethod.Post, $"http://localhost:5124/Auth/Login")
				{
					Content = postContent
				};
				var response = await httpClient.SendAsync(httpMessage);
				if (response.IsSuccessStatusCode)
				{
					var result = response.Content.ReadAsStringAsync().Result;
					var responseData = JsonSerializer.Deserialize<AuthenticateResponse>(result);


					var headers = new Dictionary<string, string>();
					httpMessage = new HttpRequestMessage(HttpMethod.Post, $"http://localhost:5124/Auth/AddClaims")
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
