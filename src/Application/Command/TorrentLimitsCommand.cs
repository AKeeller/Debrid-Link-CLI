public class TorrentLimitsCommand : ApiCommand
{
	public TorrentLimitsCommand(IApiKeyProvider apiKeyProvider) : base("limits", "Show seedbox usage and limits", apiKeyProvider)
	{
		Aliases.Add("usage");

		SetActionWithClient(async client =>
		{
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