using Humanizer;
using Spectre.Console;

public static class TorrentActionSelector
{
	public static TorrentAction SelectAction(Torrent torrent) =>
		AnsiConsole.Prompt(
			new SelectionPrompt<TorrentAction>()
			.Title($"{torrent.Name} ({torrent.TotalSize.Bytes().Humanize()})")
			.AddChoices(TorrentAction.Download, TorrentAction.Delete, TorrentAction.Back)
		);
}