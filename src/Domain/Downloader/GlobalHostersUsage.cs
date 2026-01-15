public sealed class GlobalHostersUsage
{
	public required Limit UsagePercent { get; init; }
	public required Limit NextResetSeconds { get; init; }
	public required IReadOnlyList<HosterUsage> Hosters { get; init; }
}