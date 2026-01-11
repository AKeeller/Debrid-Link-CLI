public sealed class AccountSettings
{
	public bool Https { get; init; }
	public bool ThemeDark { get; init; }
	public bool HideOldLinks { get; init; }
	public string Cdn { get; init; } = default!;
}