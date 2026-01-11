public sealed class AccountInfo
{
	public string Username { get; init; } = default!;
	public string Email { get; init; } = default!;
	public bool EmailVerified { get; init; }
	public long PremiumLeft { get; init; }
	public int Pts { get; init; }

	public Uri UpgradeAccountUrl { get; init; } = default!;
	public Uri VouchersUrl { get; init; } = default!;
	public Uri EditPasswordUrl { get; init; } = default!;
	public Uri EditEmailUrl { get; init; } = default!;
	public Uri ViewSessidUrl { get; init; } = default!;

	public AccountType AccountType { get; init; }
	public DateOnly RegisterDate { get; init; }
	public bool ServerDetected { get; init; }

	public AccountSettings Settings { get; init; } = default!;
}