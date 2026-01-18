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
			Header = new PanelHeader("Debrid-Link CLI", Justify.Center)
		};
		AnsiConsole.Write(panel);

		// No API Key found, prompt user to enter one and save it using the ConfigApiKeyProvider
		var configProvider = new ConfigApiKeyProvider();
		try
		{
			var apiKey = ApiKeyPrompt.Ask();
			configProvider.WriteApiKeyToConfig(apiKey);
			AnsiConsole.MarkupLineInterpolated($":check_mark_button: [green]API Key saved to:[/] {configProvider.ConfigPath}");
			return apiKey;
		}
		catch (OperationCanceledException) { }
		catch (Exception ex)
		{
			AnsiConsole.MarkupLineInterpolated($"[red]Failed to save API Key:[/] {ex.Message}");
		}
		return null;
	}

	public string UsageHint => string.Join("\n\n", providers.Select((provider, index) => $"[yellow]{index + 1}.[/] {provider.UsageHint}"));
}