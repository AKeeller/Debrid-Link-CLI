public class DownloaderFile : IDownloadableFile
{
	public long Created { get; set; }
	public required string Id { get; set; }
	public required string Name { get; set; }
	public required Uri Url { get; set; }
	public required Uri DownloadUrl { get; set; }
	public bool Expired { get; set; }
	public int Chunk { get; set; }
	public required string Host { get; set; }
	public long Size { get; set; }
}