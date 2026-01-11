using System.CommandLine;

public class AccountCommand : Command
{
	public AccountCommand(IApiKeyProvider apiKeyProvider) : base("account", "Display DebridLink account information") =>
		SetAction(async _ =>
		{
			var apiKey = apiKeyProvider.GetApiKey();
			if (apiKey is null)
				return 1;

			using var client = new DebridLinkClient(apiKey);
			var account = await client.GetAccountAsync();

			if (account is null)
			{
				Console.WriteLine("Failed to retrieve account information");
				return 1;
			}

			AccountInfoView.Render(account);

			return 0;
		});
}