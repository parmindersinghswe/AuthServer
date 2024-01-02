namespace Auth.Domain.Utilities
{
	public static class UrlUtility
	{
		public static string CombineUrls(string domain, string path)
		{
			Uri baseUri = new Uri(domain);

			if (!path.StartsWith("/"))
			{
				path = "/" + path;
			}

			Uri combinedUri = new Uri(baseUri, path);

			return combinedUri.ToString();
		}
		public static bool IsUrlValid(string? url)
		{
			return !string.IsNullOrEmpty(url) && (Uri.TryCreate(url, UriKind.Absolute, out Uri? result) && result != null && (result.Scheme == Uri.UriSchemeHttp || result.Scheme == Uri.UriSchemeHttps));
		}
		public static string? CoalesceUrls(params string?[] urls)
		{
			foreach (var url in urls)
			{
				if (IsUrlValid(url))
				{
					return url;
				}
			}
			return null;
		}
	}

}
