using System.CommandLine;

public class TorrentLimitsCommand : Command
{
	public TorrentLimitsCommand(IApiKeyProvider apiKeyProvider) : base("limits", "Show seedbox usage and limits")
	{
		Aliases.Add("usage");

		SetAction(async _ =>
		{
			var apiKey = apiKeyProvider.GetApiKey();
			if (apiKey is null)
				return 1;

			using var client = new DebridLinkClient(apiKey);
			var limits = await client.GetSeedboxLimitsAsync();

			if (limits is null)
			{
				Console.WriteLine("Failed to retrieve seedbox limits");
				return 1;
			}

			SeedboxLimitsView.Render(limits);

			return 0;
		});
	}
}