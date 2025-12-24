using Spectre.Console;

public class SmartApiKeyProvider(params IApiKeyProvider[] providers) : IApiKeyProvider
{
	public string? GetApiKey()
	{
		string? key = providers
			.Select(provider => provider.GetApiKey())
			.FirstOrDefault(key => !string.IsNullOrWhiteSpace(key));

		if (key is not null)
			return key;

		var panel = new Panel("[red bold]:warning: API Key Missing[/]\n\n" + UsageHint)
		{
			Border = BoxBorder.Double,
			Padding = new Padding(1, 1),
			Header = new PanelHeader("DebridLink CLI", Justify.Center)
		};
		AnsiConsole.Write(panel);
		return null;
	}

	public string UsageHint => string.Join("\n\n", providers.Select((provider, index) => $"[yellow]{index + 1}.[/] {provider.UsageHint}"));
}