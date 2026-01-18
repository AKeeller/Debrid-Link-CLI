using System.CommandLine;

public abstract class ApiCommand : Command
{
	private readonly IApiKeyProvider _apiKeyProvider;

	protected ApiCommand(string name, string description, IApiKeyProvider apiKeyProvider) : base(name, description) => _apiKeyProvider = apiKeyProvider;

	protected void SetActionWithClient(Func<ParseResult, DebridLinkClient, Task<int>> action) =>
		SetAction(async parseResult =>
		{
			var apiKey = _apiKeyProvider.GetApiKey();
			if (apiKey is null)
				return 1;

			using var client = new DebridLinkClient(apiKey);
			return await action(parseResult, client);
		});

	protected void SetActionWithClient(Func<DebridLinkClient, Task<int>> action) => SetActionWithClient((_, client) => action(client));
}