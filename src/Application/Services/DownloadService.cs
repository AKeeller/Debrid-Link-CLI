using Spectre.Console;

public static class DownloadService
{
	private static async Task DownloadOneAsync(HttpClient http, TorrentFile file, string outputFolder, ProgressTask task, ProgressTask? globalTask = null)
	{
		var response = await http.GetAsync(file.DownloadUrl!, HttpCompletionOption.ResponseHeadersRead);
		response.EnsureSuccessStatusCode();

		Directory.CreateDirectory(outputFolder);

		var filePath = Path.Combine(outputFolder, file.Name);

		await using var input = await response.Content.ReadAsStreamAsync();
		await using var output = File.Create(filePath);

		var buffer = new byte[81920];
		long totalRead = 0;
		int read;

		var totalBytes = response.Content.Headers.ContentLength ?? -1L;
		while ((read = await input.ReadAsync(buffer)) > 0)
		{
			await output.WriteAsync(buffer.AsMemory(0, read));
			totalRead += read;

			task.Value = totalRead;
			globalTask?.Increment(read);
		}
	}

	public static async Task DownloadAllAsync(IEnumerable<TorrentFile> files, string outputFolder)
	{
		using var http = new HttpClient();

		long totalBytes = files.Sum(f => f.Size);

		await AnsiConsole.Progress()
			.AutoClear(false)
			.HideCompleted(false)
			.Columns(new ProgressColumn[]
			{
				new TaskDescriptionColumn(),
				new ProgressBarColumn(),
				new PercentageColumn(),
				new RemainingTimeColumn(),
				new TransferSpeedColumn()
			})
			.StartAsync(async ctx =>
			{
				var tasks = files
					.Where(file => !string.IsNullOrWhiteSpace(file.DownloadUrl.AbsoluteUri))
					.Select(file =>
					{
						var task = ctx.AddTask(file.Name, maxValue: file.Size);
						return (File: file, Task: task);
					})
					.ToList();

				var globalTask = tasks.Count > 1 ? ctx.AddTask("[bold yellow]TOTAL[/]", maxValue: totalBytes) : null;

				await Task.WhenAll(tasks.Select(t => DownloadOneAsync(http, t.File, outputFolder, t.Task, globalTask)));
			});
	}
}