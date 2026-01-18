public class AccountCommand : ApiCommand
{
	public AccountCommand(IApiKeyProvider apiKeyProvider) : base("account", "Display DebridLink account information", apiKeyProvider) =>
		SetActionWithClient(async client =>
		{
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