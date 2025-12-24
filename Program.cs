var apiKey = new SmartApiKeyProvider(
	new ArgsApiKeyProvider(args),
	new EnvApiKeyProvider(),
	new StdinApiKeyProvider(),
	new ConfigApiKeyProvider()
).GetApiKey();

if (apiKey is null)
	return 1;

using var client = new DebridLinkClient(apiKey);
var account = await client.GetAccountAsync();
var torrents = await client.GetTorrentsAsync();

AccountInfoView.Render(account);

Console.WriteLine();

var chosenTorrent = TorrentSelector.SelectFrom(torrents);
await DownloadService.DownloadAllAsync(chosenTorrent.Files, "downloads");

return 0;