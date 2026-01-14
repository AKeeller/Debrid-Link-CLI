using System.CommandLine;

public class TorrentAddCommand : Command
{
	private Option<string> _torrentLinkOption = new("--link")
	{
		Aliases = { "-l" },
		Description = "Link to a magnet or torrent file to add to DebridLink",
		Required = false
	};

	private Option<FileInfo> _torrentFileOption = new("--file")
	{
		Aliases = { "-f" },
		Description = "Torrent file path to add to DebridLink",
		Required = false
	};

	public TorrentAddCommand(IApiKeyProvider apiKeyProvider) : base("add", "Add a torrent to DebridLink")
	{
		Options.Add(_torrentLinkOption);
		Options.Add(_torrentFileOption);

		SetAction(async parseResult =>
		{
			var apiKey = apiKeyProvider.GetApiKey();
			if (apiKey is null)
				return 1;

			using var client = new DebridLinkClient(apiKey);

			var torrentLink = parseResult.GetValue(_torrentLinkOption);
			var torrentFile = parseResult.GetValue(_torrentFileOption);

			if (torrentLink is not null)
			{
				var success = await client.AddTorrentAsync(torrentLink);
				Console.WriteLine($"Torrent was {(success ? "successfully" : "not")} added.");
				return 0;
			}

			if (torrentFile is not null)
			{
				var success = await client.AddTorrentAsync(torrentFile);
				Console.WriteLine($"Torrent was {(success ? "successfully" : "not")} added.");
				return 0;
			}

			await Parse("-h").InvokeAsync();
			return 1;
		});
	}
}