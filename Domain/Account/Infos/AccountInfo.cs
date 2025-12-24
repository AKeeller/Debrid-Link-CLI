using System.Text.Json.Serialization;

public class AccountInfo
{
	[JsonPropertyName("Pseudo")]
	public string Username { get; set; } = string.Empty;
	public string Email { get; set; } = string.Empty;
	public bool EmailVerified { get; set; }
	public int AccountType { get; set; }
	public long PremiumLeft { get; set; }
	public int Pts { get; set; }
	public int TrafficShare { get; set; }
	public string VouchersUrl { get; set; } = string.Empty;
	public string EditPasswordUrl { get; set; } = string.Empty;
	public string EditEmailUrl { get; set; } = string.Empty;
	public string ViewSessidUrl { get; set; } = string.Empty;
	public string UpgradeAccountUrl { get; set; } = string.Empty;
	public string RegisterDate { get; set; } = string.Empty;
	public bool ServerDetected { get; set; }
	public AccountSettings Settings { get; set; } = new();
	public string AvatarUrl { get; set; } = string.Empty;
}