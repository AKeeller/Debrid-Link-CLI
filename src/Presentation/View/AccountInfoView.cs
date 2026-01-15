using DebridLinkCLI.Presentation.Formatting;
using Spectre.Console;

public static class AccountInfoView
{
	public static void Render(AccountInfo account)
	{
		var root = new Tree($"[bold cyan]{account.Username}[/]");

		root.AddNode(BuildBasicInfo(account));
		root.AddNode(BuildAccountStatus(account));

		AnsiConsole.Write(root);
	}

	private static TreeNode BuildBasicInfo(AccountInfo a)
	{
		var node = new TreeNode(new Markup("[yellow]Basic info[/]"));

		node.AddNode($"Email: {a.Email}");
		node.AddNode($"Email verified: {FormatBool(a.EmailVerified)}");
		node.AddNode($"Registered: {a.RegisterDate}");

		return node;
	}

	private static TreeNode BuildAccountStatus(AccountInfo a)
	{
		var node = new TreeNode(new Markup("[yellow]Account[/]"));

		node.AddNode($"Type: {a.AccountType}");

		if (a.AccountType is AccountType.Premium)
			node.AddNode($"Premium left: {TimeFormatter.FormatSeconds(a.PremiumLeft)}");

		node.AddNode($"Points: {a.Pts}");

		return node;
	}

	private static string FormatBool(bool value) => value ? "[green]Yes[/]" : "[red]No[/]";
}