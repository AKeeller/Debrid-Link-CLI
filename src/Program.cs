using System.CommandLine;

var apiKeyProvider = new SmartApiKeyProvider(
	new EnvApiKeyProvider(),
	new ConfigApiKeyProvider()
);

var rootCommand = new RootCommand()
{
	Subcommands =
	{
		new AccountCommand(apiKeyProvider),
		new TorrentCommand(apiKeyProvider),
		new DownloaderCommand(apiKeyProvider)
	}
};

return await rootCommand.Parse(args).InvokeAsync();