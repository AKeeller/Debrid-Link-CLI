using System.Text.Json;

public class ConfigApiKeyProvider : IApiKeyProvider
{
	private readonly string _configPath;

	public ConfigApiKeyProvider(string? configPath = null) =>
		_configPath = configPath ?? Path.Combine(
			Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
			"Debrid-Link",
			"config.json"
		);

	public string? GetApiKey()
	{
		if (!File.Exists(_configPath))
			return null;

		try
		{
			var config = JsonSerializer.Deserialize<Config>(File.ReadAllText(_configPath));
			return config?.ApiKey;
		}
		catch (JsonException)
		{
			return null;
		}
	}

	public string UsageHint =>
		$"[yellow]Config file:[/] Create [yellow]{_configPath}[/]\n" +
		"Example content:\n[green]{\n    \"ApiKey\": \"your_api_key_here\"\n}[/]";
}