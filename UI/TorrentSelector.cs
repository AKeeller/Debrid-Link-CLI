using Humanizer;
using Spectre.Console;

public static class TorrentSelector
{
	public static Torrent SelectFrom(IEnumerable<Torrent> torrents)
	{
		var prompt = new SelectionPrompt<Torrent>
		{
			Title = "Select torrent to download",
			Converter = t => $"{t.Name} ({t.TotalSize?.Bytes().Humanize() ?? "Unknown size"})"
		};

		prompt.AddChoices(torrents);
		return AnsiConsole.Prompt(prompt);
	}
}