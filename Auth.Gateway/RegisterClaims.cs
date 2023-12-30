using System.Text;

namespace Auth.Gateway
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
				Thread.Sleep(500);
				var postData = new StringContent(string.Empty, Encoding.UTF8, "application/json");
				using (var httpClient = new HttpClient())
				{
					var httpMessage = new HttpRequestMessage(HttpMethod.Post, "http://localhost:5257/MicroServiceConfigurations/CreatePolicy")
					{
						Content = postData
					};
					var response = await httpClient.SendAsync(httpMessage);

				}

			}
			FirstRender = false;
			await _next(context);
		}
	}
}
