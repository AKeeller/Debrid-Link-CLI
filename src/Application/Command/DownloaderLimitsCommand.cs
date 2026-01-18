public class DownloaderLimitsCommand : ApiCommand
{
	public DownloaderLimitsCommand(IApiKeyProvider apiKeyProvider) : base("limits", "Show downloader limits and usage", apiKeyProvider)
	{
		Aliases.Add("usage");

		SetActionWithClient(async client =>
		{
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