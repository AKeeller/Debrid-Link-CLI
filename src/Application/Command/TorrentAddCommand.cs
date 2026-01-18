using System.CommandLine;

public class TorrentAddCommand : ApiCommand
{
	private Option<FileInfo> _torrentLinksFromFileOption = new("--links-from-file")
	{
		Description = "Path to a file containing multiple torrent links to add to DebridLink",
		HelpName = "file",
		Required = false
	};

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

	public TorrentAddCommand(IApiKeyProvider apiKeyProvider) : base("add", "Add a torrent to DebridLink", apiKeyProvider)
	{
		Options.Add(_torrentLinksFromFileOption);
		Options.Add(_torrentLinkOption);
		Options.Add(_torrentFileOption);

		SetActionWithClient(async (parseResult, client) =>
		{
			var torrentLinksFile = parseResult.GetValue(_torrentLinksFromFileOption);
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

			if (torrentLinksFile is not null)
			{
				var tasks = new List<Task>();
				await foreach (var link in File.ReadLinesAsync(torrentLinksFile.FullName).Where(link => !string.IsNullOrWhiteSpace(link)))
				{
					tasks.Add(Task.Run(async () =>
					{
						bool success = await client.AddTorrentAsync(link);
						Console.WriteLine($"{link} was {(success ? "successfully" : "not")} added.");
					}));
				}
				await Task.WhenAll(tasks);
				return 0;
			}

			await Parse("-h").InvokeAsync();
			return 1;
		});
	}
}