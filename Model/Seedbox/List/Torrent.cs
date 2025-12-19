public class Torrent
{
	public string? Id { get; set; }
	public string? Name { get; set; }
	public long? AddedDate { get; set; }
	public string? HashString { get; set; }
	public double? UploadRatio { get; set; }
	public string? ServerId { get; set; }
	public bool? Wait { get; set; }
	public int? PeersConnected { get; set; }
	public int? Status { get; set; }
	public long? TotalSize { get; set; }
	public int? PercentDownload { get; set; }
	public int? DownloadSpeed { get; set; }
	public int? UploadSpeed { get; set; }
	public bool? IsZip { get; set; }
	public bool? SrvMaint { get; set; }
	public required List<FileItem> Files { get; set; }
	public List<Tracker>? Trackers { get; set; }
}