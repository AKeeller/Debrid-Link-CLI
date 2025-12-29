using System.CommandLine;

var apiKeyProvider = new SmartApiKeyProvider(
	new EnvApiKeyProvider(),
	new ConfigApiKeyProvider()
);

var rootCommand = new RootCommand()
{
	Subcommands =
	{
		new TorrentCommand(apiKeyProvider)
	}
};

return await rootCommand.Parse(args).InvokeAsync();