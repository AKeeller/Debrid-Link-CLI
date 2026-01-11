using System.Net.Http.Json;
using System.Text.Json;

public sealed class DebridLinkClient(string apiKey) : IDisposable
{
	private readonly HttpClient _http = HttpClient.CreateAuthenticatedHttpClient("https://debrid-link.com/api/v2/", apiKey);

	public async Task<AccountInfo> GetAccountAsync() => (await GetAsync<AccountInfo>("account/infos"))!;
	public async Task<List<Torrent>> GetTorrentsAsync() => (await GetAsync<List<Torrent>>("seedbox/list"))!;

	private async Task<T?> GetAsync<T>(string url) where T : class
	{
		var response = await _http.GetFromJsonAsync<ApiResponse<T>>(url);
		return response?.Value;
	}

	public async Task<bool> AddTorrentAsync(string url)
	{
		var payload = new { url };
		var response = await _http.PostAsJsonAsync("seedbox/add", payload);

		await using var stream = await response.Content.ReadAsStreamAsync();
		using var doc = await JsonDocument.ParseAsync(stream);

		return doc.RootElement.GetProperty("success").GetBoolean();
	}

	public void Dispose() => _http.Dispose();
}