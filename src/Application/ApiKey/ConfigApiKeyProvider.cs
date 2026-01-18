using System.Text.Json;

public class ConfigApiKeyProvider : IApiKeyProvider
{
	public readonly string ConfigPath = Path.Combine(
			Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
			"Debrid-Link",
			"config.json"
		);

	public string? GetApiKey()
	{
		if (!File.Exists(ConfigPath))
			return null;

		try
		{
			var config = JsonSerializer.Deserialize<Config>(File.ReadAllText(ConfigPath));
			return config?.ApiKey;
		}
		catch (JsonException)
		{
			return null;
		}
	}

	public string UsageHint =>
		$"[yellow]Config file:[/] Create [yellow]{ConfigPath}[/]\n" +
		"Example content:\n[green]{\n    \"ApiKey\": \"your_api_key_here\"\n}[/]";

	public void WriteApiKeyToConfig(string apiKey)
	{
		var configDir = Path.GetDirectoryName(ConfigPath);
		Directory.CreateDirectory(configDir!);

		var config = new Config { ApiKey = apiKey };
		var json = JsonSerializer.Serialize(config, new JsonSerializerOptions { WriteIndented = true });
		File.WriteAllText(ConfigPath, json);
	}
}