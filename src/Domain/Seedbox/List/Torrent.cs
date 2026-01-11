public sealed class Torrent
{
	public string Id { get; init; } = default!;
	public string Name { get; init; } = default!;
	public long Created { get; init; }
	public string HashString { get; init; } = default!;
	public double UploadRatio { get; init; }
	public string ServerId { get; init; } = default!;
	public bool Wait { get; init; }
	public int PeersConnected { get; init; }
	public int Status { get; init; }
	public long TotalSize { get; init; }
	public int DownloadPercent { get; init; }
	public long DownloadSpeed { get; init; }
	public long UploadSpeed { get; init; }
	public bool IsZip { get; init; }
	public bool SrvMaint { get; init; }

	public IReadOnlyList<TorrentFile> Files { get; init; } = [];
	public IReadOnlyList<Tracker> Trackers { get; init; } = [];
}