using System.Text.Json.Serialization;

public class FileItem
{
	public required string Id { get; set; }
	public required string Name { get; set; }

	[JsonPropertyName("length")]
	public long Size { get; set; }
	public string? DownloadUrl { get; set; }
	public int? DownloadPercent { get; set; }
}