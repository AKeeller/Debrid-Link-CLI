public sealed class SeedboxLimits
{
	public required Limit UsagePercent { get; init; }
	public required Limit NextResetSeconds { get; init; }
	public required Limit DayCount { get; init; }
	public required Limit DaySize { get; init; }
	public required Limit MonthCount { get; init; }
	public required Limit MonthSize { get; init; }
	public required Limit TorrentSize { get; init; }
	public required Limit TorrentFiles { get; init; }
	public required Limit PrivateRatio { get; init; }
	public required Limit PublicRatio { get; init; }
	public required Limit SeedDurationDays { get; init; }
	public required Limit ActiveTransferCount { get; init; }
	public required Limit ViewCount { get; init; }
}