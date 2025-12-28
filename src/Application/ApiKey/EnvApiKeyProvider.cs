public class EnvApiKeyProvider : IApiKeyProvider
{
	public string? GetApiKey() => Environment.GetEnvironmentVariable("DEBRID_LINK_API_KEY");

	public string UsageHint =>
		"[yellow]Environment variable:[/] Set [yellow]DEBRID_LINK_API_KEY[/]\n" +
		"Example (Linux/macOS): [green]export DEBRID_LINK_API_KEY=your_api_key[/]\n" +
		"Example (Windows PowerShell): [green]setx DEBRID_LINK_API_KEY your_api_key[/]";
}