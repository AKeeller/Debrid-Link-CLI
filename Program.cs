DotNetEnv.Env.Load();

var apiKey = Environment.GetEnvironmentVariable("DEBRID_LINK_API_KEY") ?? throw new InvalidOperationException("Missing DEBRID_LINK_API_KEY");

using var client = new DebridLinkClient(apiKey);
var account = await client.GetAccountAsync();
var torrents = await client.GetTorrentsAsync();

AccountInfoView.Render(account);

var chosenTorrent = TorrentSelector.SelectFrom(torrents);
await DownloadService.DownloadAllAsync(chosenTorrent.Files, "downloads");