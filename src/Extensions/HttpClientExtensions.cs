using System.Net.Http.Headers;

public static class HttpClientExtensions
{
	extension(HttpClient)
	{
		public static HttpClient CreateAuthenticatedHttpClient(string baseUrl, string apiKey)
		{
			var client = new HttpClient
			{
				BaseAddress = new Uri(baseUrl)
			};

			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

			return client;
		}
	}
}