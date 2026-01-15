using System.CommandLine;

public class DownloaderCommand : Command
{
	public DownloaderCommand(IApiKeyProvider apiKeyProvider) : base("downloader", "List and download files from downloader links")
	{
		Subcommands.Add(new DownloaderLimitsCommand(apiKeyProvider));

		SetAction(async _ =>
		{
			var apiKey = apiKeyProvider.GetApiKey();
			if (apiKey is null)
				return 1;

			using var client = new DebridLinkClient(apiKey);
			var downloaderFiles = await client.GetDownloaderFilesAsync().ToListAsync();

			while (true)
			{
				if (downloaderFiles is null || downloaderFiles.Count is 0)
				{
					Console.WriteLine("No downloader links found");
					return 1;
				}

				var chosenDownloaderFiles = DownloaderSelector.SelectFrom(downloaderFiles);
				var action = DownloaderActionSelector.SelectAction(chosenDownloaderFiles);

				if (action == FileAction.Delete)
				{
					var success = await client.RemoveDownloaderFilesAsync(chosenDownloaderFiles);
					if (success)
						downloaderFiles.RemoveAll(file => chosenDownloaderFiles.Any(file2 => ReferenceEquals(file, file2)));
				}

				if (action == FileAction.Download)
				{
					await DownloadService.DownloadAllAsync(chosenDownloaderFiles, ".");
					break;
				}
			}

			return 0;
		});
	}
}