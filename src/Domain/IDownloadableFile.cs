public interface IDownloadableFile
{
	string Name { get; }
	Uri DownloadUrl { get; }
	long Size { get; }
}