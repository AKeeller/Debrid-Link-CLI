using Humanizer;
using Spectre.Console;

public static class DownloaderActionSelector
{
	public static FileAction SelectAction(List<DownloaderFile> downloaderFiles) =>
		AnsiConsole.Prompt(
			new SelectionPrompt<FileAction>()
			.Title(string.Join('\n', downloaderFiles.Select(f => $"{f.Name} ({f.Size.Bytes().Humanize()})")) )
			.AddChoices(FileAction.Download, FileAction.Delete, FileAction.Back)
		);
}