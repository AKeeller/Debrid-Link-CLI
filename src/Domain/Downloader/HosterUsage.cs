public sealed class HosterUsage
{
	public required string Name { get; init; }
	public required Limit DaySize { get; init; }
	public required Limit DayCount { get; init; }
}