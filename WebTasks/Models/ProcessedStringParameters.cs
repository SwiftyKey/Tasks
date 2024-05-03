namespace WebTasks.Models;

public class ProcessedStringParameters
{
	public string? SortType { get; set; }
	public string? ProcessedString { get; set; }
	public Dictionary<char, int>? Counter { get; set; }
	public string? LargestSubstring { get; set; }
	public string? SortedProcessedString { get; set; }
	public Dictionary<string, object>? Shortened { get; set; }
}
