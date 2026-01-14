using System.CommandLine;

public class DownloaderCommand : Command
{
	public DownloaderCommand(IApiKeyProvider apiKeyProvider) : base("downloader", "List and download files from downloader links")
	{
		SetAction(async _ =>
		{
			var apiKey = apiKeyProvider.GetApiKey();
			if (apiKey is null)
				return 1;

			using var client = new DebridLinkClient(apiKey);
			var downloaderFiles = await client.GetDownloaderFilesAsync();

			if (downloaderFiles is null || downloaderFiles.Count is 0)
			{
				Console.WriteLine("No downloader links found");
				return 1;
			}

			var chosenDownloaderFiles = DownloaderSelector.SelectFrom(downloaderFiles);
			await DownloadService.DownloadAllAsync(chosenDownloaderFiles, ".");

			return 0;
		});
	}
}