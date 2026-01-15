using System.CommandLine;

public class DownloaderLimitsCommand : Command
{
	public DownloaderLimitsCommand(IApiKeyProvider apiKeyProvider) : base("limits", "Show downloader limits and usage")
	{
		Aliases.Add("usage");

		SetAction(async _ =>
		{
			var apiKey = apiKeyProvider.GetApiKey();
			if (apiKey is null)
				return 1;

			using var client = new DebridLinkClient(apiKey);
			var limits = await client.GetHostersUsageAsync();
			if (limits is null)
			{
				Console.WriteLine("Failed to retrieve downloader limits");
				return 1;
			}

			DownloaderLimitsView.Render(limits);

			return 0;
		});
	}
}