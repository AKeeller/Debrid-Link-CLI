public sealed class TorrentFile : IDownloadableFile
{
	public string Id { get; init; } = default!;
	public string Name { get; init; } = default!;
	public long Size { get; init; }
	public Uri DownloadUrl { get; init; } = default!;
	public int DownloadPercent { get; init; }
}