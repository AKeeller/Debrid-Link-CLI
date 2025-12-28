public interface IApiKeyProvider
{
	string? GetApiKey();
	string UsageHint { get; }
}