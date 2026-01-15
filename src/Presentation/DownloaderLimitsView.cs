using DebridLinkCLI.Presentation.Formatting;
using Spectre.Console;

public static class DownloaderLimitsView
{
	public static void Render(GlobalHostersUsage usage)
	{
		var root = new Tree("[bold cyan]Downloader Limits and Usage[/]");

		root.AddNode(BuildBasicInfo(usage));

		var hosters = BuildHostersNode(usage.Hosters);
		if(hosters.Nodes.Count > 0)
			root.AddNode(hosters);

		AnsiConsole.Write(root);
	}

	private static TreeNode BuildBasicInfo(GlobalHostersUsage usage)
	{
		var node = new TreeNode(new Markup("[yellow]Basic info[/]"));

		node.AddNode($"Usage: {usage.UsagePercent.Current}%");
		node.AddNode($"Next reset in: {TimeFormatter.FormatSeconds(usage.NextResetSeconds.Value)}");

		return node;
	}

	private static TreeNode BuildHostersNode(IEnumerable<HosterUsage> hosters)
	{
		var mainNode = new TreeNode(new Markup("[yellow]Hosters[/]"));

		foreach (var hoster in hosters)
		{
			var hosterNode = new TreeNode(new Markup($"[green]{hoster.Name}[/]"));
			hosterNode.AddNode($"Used: {hoster.DaySize.Current}");
			hosterNode.AddNode($"Limit: {hoster.DaySize.Value}");
			mainNode.AddNode(hosterNode);
		}

		return mainNode;
	}
}