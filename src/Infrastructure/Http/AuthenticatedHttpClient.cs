using System.Net.Http.Headers;

public static class AuthenticatedHttpClient
{
	public static HttpClient Create(string baseUrl, string apiKey)
	{
		var client = new HttpClient
		{
			BaseAddress = new Uri(baseUrl)
		};

		client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

		return client;
	}
}