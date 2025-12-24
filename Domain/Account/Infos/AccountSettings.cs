public class AccountSettings
{
	public int Https { get; set; }
	public int ThemeDark { get; set; }
	public int HideOldLinks { get; set; }
	public string Cdn { get; set; } = string.Empty;
	public string TwofaType { get; set; } = string.Empty;
	public int EmailsNews { get; set; }
	public int EmailsAccount { get; set; }
	public int EmailsSupport { get; set; }
}