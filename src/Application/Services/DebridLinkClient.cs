using System.Net.Http.Json;
using System.Text.Json;

public sealed class DebridLinkClient(string apiKey) : IDisposable
{
	private readonly HttpClient _http = HttpClient.CreateAuthenticatedHttpClient("https://debrid-link.com/api/v2/", apiKey);

	public async Task<AccountInfo?> GetAccountAsync() => (await _http.GetFromJsonAsync<ApiResponse<AccountInfo>>("account/infos"))?.Value;
	public async Task<List<Torrent>?> GetTorrentsAsync() => (await _http.GetFromJsonAsync<ApiResponse<List<Torrent>>>("seedbox/list"))?.Value;
	public async Task<SeedboxLimits?> GetSeedboxLimitsAsync() => (await _http.GetFromJsonAsync<ApiResponse<SeedboxLimits>>("seedbox/limits"))?.Value;

	public async Task<bool> AddTorrentAsync(string url)
	{
		var payload = new { url };
		var response = await _http.PostAsJsonAsync("seedbox/add", payload);

		await using var stream = await response.Content.ReadAsStreamAsync();
		using var doc = await JsonDocument.ParseAsync(stream);

		return doc.RootElement.GetProperty("success").GetBoolean();
	}

	public async Task<bool> AddTorrentAsync(FileInfo torrentFile)
	{
		if (!torrentFile.Exists)
			return false;

		await using var stream = torrentFile.OpenRead();
		using var content = new MultipartFormDataContent();
		var fileContent = new StreamContent(stream);
		content.Add(fileContent, "file", torrentFile.Name);

		var response = await _http.PostAsync("seedbox/add", content);

		await using var responseStream = await response.Content.ReadAsStreamAsync();
		using var doc = await JsonDocument.ParseAsync(responseStream);

		return doc.RootElement.GetProperty("success").GetBoolean();
	}

	public async Task<bool> RemoveTorrentAsync(Torrent torrent)
	{
		var response = await _http.DeleteAsync($"seedbox/{torrent.Id}/remove");

		await using var stream = await response.Content.ReadAsStreamAsync();
		using var doc = await JsonDocument.ParseAsync(stream);

		return doc.RootElement.GetProperty("success").GetBoolean();
	}

	public void Dispose() => _http.Dispose();
}