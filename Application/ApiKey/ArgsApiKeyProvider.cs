public class ArgsApiKeyProvider(string[] args) : IApiKeyProvider
{
	public string? GetApiKey()
	{
		var index = Array.IndexOf(args, "--api-key");
		if (index >= 0 && index + 1 < args.Length)
			return args[index + 1];

		return null;
	}

	public string UsageHint =>
		"[yellow]Command-line argument:[/] Use [yellow]--api-key[/]\n" +
		"Example: [green]dlcli --api-key your_api_key[/]";
}