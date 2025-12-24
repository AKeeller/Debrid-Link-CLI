public class StdinApiKeyProvider : IApiKeyProvider
{
	public string? GetApiKey() => Console.IsInputRedirected ? Console.In.ReadLine() : null;

	public string UsageHint =>
		"[yellow]Standard input (stdin):[/] Pipe the key directly\n" +
		"Example: [green]echo your_api_key | dlcli[/]";
}