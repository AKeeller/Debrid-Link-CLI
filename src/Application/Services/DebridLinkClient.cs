using System.Net.Http.Json;

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

		var result = await response.Content.ReadFromJsonAsync<ApiResponse<object>>();

		return result?.Success ?? false;
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

		var result = await response.Content.ReadFromJsonAsync<ApiResponse<object>>();

		return result?.Success ?? false;
	}

	public async Task<bool> RemoveTorrentAsync(Torrent torrent)
	{
		var response = await _http.DeleteAsync($"seedbox/{torrent.Id}/remove");

		var result = await response.Content.ReadFromJsonAsync<ApiResponse<object>>();

		return result?.Success ?? false;
	}

	public async IAsyncEnumerable<DownloaderFile> GetDownloaderFilesAsync()
	{
		for (int page = 0; page != -1;)
		{
			var response = await _http.GetFromJsonAsync<ApiResponse<List<DownloaderFile>>>($"downloader/list?page={page}&perPage=100");

			if (response?.Value is null)
				yield break;

			foreach (var file in response.Value)
				yield return file;

			page = response.Pagination?.Next ?? -1;
		}
	}

	public async Task<bool> RemoveDownloaderFilesAsync(params IEnumerable<DownloaderFile> downloaderFiles)
	{
		var idsCsv = string.Join(",", downloaderFiles.Select(f => f.Id));
		var response = await _http.DeleteAsync($"downloader/{idsCsv}/remove");

		var result = await response.Content.ReadFromJsonAsync<ApiResponse<object>>();

		return result?.Success ?? false;
	}

	public void Dispose() => _http.Dispose();
}