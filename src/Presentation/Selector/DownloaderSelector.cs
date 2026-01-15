using Humanizer;
using Spectre.Console;

public static class DownloaderSelector
{
	public static List<DownloaderFile> SelectFrom(IEnumerable<DownloaderFile> downloaderFiles) =>
		AnsiConsole.Prompt(
			new MultiSelectionPrompt<DownloaderFile>()
			.AddChoices(downloaderFiles)
			.UseConverter(downloader => Markup.Escape($"{downloader.Name} ({downloader.Size.Bytes().Humanize() ?? "Unknown size"})"))
		);
}