namespace DebridLinkCLI.Presentation.Formatting;

internal static class TimeFormatter
{
	public static string Format(TimeSpan timeSpan)
	{
		if (timeSpan < TimeSpan.Zero)
			return "[red]Expired[/]";

		return $"{timeSpan.Days}d {timeSpan.Hours}h {timeSpan.Minutes}m";
	}

	public static string FormatSeconds(double seconds) => Format(TimeSpan.FromSeconds(seconds));
}