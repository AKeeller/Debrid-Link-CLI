using Humanizer;
using Spectre.Console;

public static class SeedboxLimitsView
{
	public static void Render(SeedboxLimits seedboxLimits)
	{
		var root = new Tree("[yellow]Usage and limits[/]");

		root.AddNode(BuildBasicInfo(seedboxLimits));
		root.AddNode(BuildStatsNode(seedboxLimits));

		AnsiConsole.Write(root);
	}

	private static TreeNode BuildBasicInfo(SeedboxLimits limits)
	{
		var node = new TreeNode(new Markup("[yellow]Basic info[/]"));

		node.AddNode($"Usage: {limits.UsagePercent.Current}%");
		node.AddNode($"Next reset in: {FormatSeconds(limits.NextResetSeconds.Value)}");

		return node;
	}

	private static TreeNode BuildStatsNode(SeedboxLimits limits)
	{
		var node = new TreeNode(new Markup("[yellow]Statistics[/]"));

		node.AddNode($"Active transfers: {limits.ActiveTransferCount.Current} / {limits.ActiveTransferCount.Value}");
		node.AddNode($"Month count: {limits.MonthCount.Current} / {limits.MonthCount.Value}");
		node.AddNode($"Month size: {limits.MonthSize.Current.Bytes().Humanize()} / {limits.MonthSize.Value.Bytes().Humanize()}");

		return node;
	}

	private static string FormatSeconds(double seconds)
	{
		var timeSpan = TimeSpan.FromSeconds(seconds);

		if (timeSpan < TimeSpan.Zero)
			return "[red]Expired[/]";

		return $"{timeSpan.Days}d {timeSpan.Hours}h {timeSpan.Minutes}m";
	}
}