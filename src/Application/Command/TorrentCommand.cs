public class TorrentCommand : ApiCommand
{
	public TorrentCommand(IApiKeyProvider apiKeyProvider) : base("torrent", "List and download torrent files", apiKeyProvider)
	{
		Aliases.Add("seedbox");

		Subcommands.Add(new TorrentAddCommand(apiKeyProvider));
		Subcommands.Add(new TorrentLimitsCommand(apiKeyProvider));

		SetActionWithClient(async client =>
		{
			var torrents = await client.GetTorrentsAsync().ToListAsync();

			while (true)
			{
				if (torrents is null || torrents.Count is 0)
				{
					Console.WriteLine("No torrents found");
					return 1;
				}

				var chosenTorrent = TorrentSelector.SelectFrom(torrents);
				var action = TorrentActionSelector.SelectAction(chosenTorrent);

				if (action == FileAction.Delete)
				{
					var success = await client.RemoveTorrentAsync(chosenTorrent);
					if (success)
						torrents.Remove(chosenTorrent);
				}

				if (action == FileAction.Download)
				{
					await DownloadService.DownloadAllAsync(chosenTorrent.Files, ".");
					break;
				}
			}

			return 0;
		});
	}
}