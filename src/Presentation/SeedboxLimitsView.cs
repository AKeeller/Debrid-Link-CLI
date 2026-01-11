using Spectre.Console;

public static class SeedboxLimitsView
{
	public static void Render(SeedboxLimits seedboxLimits)
	{
		var root = new Tree("[yellow]Usage and limits[/]");

		root.AddNode($"Usage: {seedboxLimits.UsagePercent.Current}%");
		root.AddNode($"Next reset in: {FormatSeconds(seedboxLimits.NextResetSeconds.Value)}");

		AnsiConsole.Write(root);
	}

	private static string FormatSeconds(double seconds)
	{
		var timeSpan = TimeSpan.FromSeconds(seconds);

		if (timeSpan < TimeSpan.Zero)
			return "[red]Expired[/]";

		return $"{timeSpan.Days}d {timeSpan.Hours}h {timeSpan.Minutes}m";
	}
}