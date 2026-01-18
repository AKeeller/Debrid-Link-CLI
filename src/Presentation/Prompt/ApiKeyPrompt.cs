using Spectre.Console;

public static class ApiKeyPrompt
{
	public static string Ask()
	{
		AnsiConsole.MarkupLine("\n[yellow]You can enter your API Key below. It will be saved to the config file for future use.[/]");

		var apiKey = AnsiConsole.Prompt(
			new TextPrompt<string>("Please enter your API Key:")
				.PromptStyle("green")
				.Secret()
				.AllowEmpty()
			);

		if (string.IsNullOrWhiteSpace(apiKey))
		{
			AnsiConsole.MarkupLine("[red]No API Key entered. Exiting.[/]");
			throw new OperationCanceledException("No API Key entered.");
		}

		if(apiKey.Length < 32)
		{
			AnsiConsole.MarkupLine("[red]The API Key entered seems too short. Please check and try again.[/]");
			throw new OperationCanceledException("Too short API Key entered.");
		}

		return apiKey;
	}
}