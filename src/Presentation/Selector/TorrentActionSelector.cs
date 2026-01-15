using Humanizer;
using Spectre.Console;

public static class TorrentActionSelector
{
	public static FileAction SelectAction(Torrent torrent) =>
		AnsiConsole.Prompt(
			new SelectionPrompt<FileAction>()
			.Title($"{torrent.Name} ({torrent.TotalSize.Bytes().Humanize()})")
			.AddChoices(FileAction.Download, FileAction.Delete, FileAction.Back)
		);
}