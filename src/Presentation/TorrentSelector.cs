using Humanizer;
using Spectre.Console;

public static class TorrentSelector
{
	public static Torrent SelectFrom(IEnumerable<Torrent> torrents) =>
		AnsiConsole.Prompt(
			new SelectionPrompt<Torrent>()
			.AddChoices(torrents)
			.UseConverter(torrent => $"{torrent.Name} ({torrent.TotalSize.Bytes().Humanize() ?? "Unknown size"})")
		);
}