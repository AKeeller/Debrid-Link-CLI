public class ApiResponse<T>
{
	public bool Success { get; set; }
	public T? Value { get; set; }
	public string? Error { get; set; }
	public Pagination? Pagination { get; set; }
}