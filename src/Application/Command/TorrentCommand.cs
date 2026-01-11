using System.CommandLine;

public class TorrentCommand : Command
{
	public TorrentCommand(IApiKeyProvider apiKeyProvider) : base("torrent", "List and download torrent files")
	{
		Subcommands.Add(new TorrentAddCommand(apiKeyProvider));

		SetAction(async _ =>
		{
			var apiKey = apiKeyProvider.GetApiKey();
			if (apiKey is null)
				return 1;

			using var client = new DebridLinkClient(apiKey);
			var account = await client.GetAccountAsync();
			var torrents = await client.GetTorrentsAsync();

			if (account is null)
			{
				Console.WriteLine("Failed to retrieve account information");
				return 1;
			}

			AccountInfoView.Render(account);

			if (torrents is null || torrents.Count is 0)
			{
				Console.WriteLine("No torrents found");
				return 1;
			}

			Console.WriteLine();

			var chosenTorrent = TorrentSelector.SelectFrom(torrents);
			await DownloadService.DownloadAllAsync(chosenTorrent.Files, ".");
			return 0;
		});
	}
}