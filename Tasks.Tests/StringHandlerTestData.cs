using Tasks.Sorts;

namespace Tasks.Tests;

public static class StringHandlerTestData
{
	public static TestCaseData[] IncorrectChars { get; set; } =
	{
		new TestCaseData("AvfsfB", new List<string>() {"A", "B"}).SetName("Uppercase chars"),
		new TestCaseData("0ab1cb2", new List<string>() {"0", "1", "2"}).SetName("Numbers"),
		new TestCaseData("абыcb", new List<string>() {"а", "б", "ы"}).SetName("Russian chars"),
		new TestCaseData("fa,_sfs;", new List<string>() {",", "_", ";"}).SetName("Other chars"),
		new TestCaseData("Ab9ыФи,s", new List<string>() {"A", "9", "ы", "Ф", "и", ","}).SetName("All cases together")
	};

	public static TestCaseData[] Counter { get; set; } =
	{
		new TestCaseData("abfe", new Dictionary<char, int>
		{
			{ 'a', 1 },
			{ 'b', 1 },
			{ 'f', 1 },
			{ 'e', 1 }
		}).SetName("Counter for string with even length"),
		new TestCaseData("abbce", new Dictionary<char, int>
		{
			{ 'a', 2 },
			{ 'b', 4 },
			{ 'c', 2 },
			{ 'e', 2 }
		}).SetName("Counter for string with odd length")
	};

	public static TestCaseData[] Sorts { get; set; } =
	{
		new TestCaseData("bfeada", new QuickSort<char>(), "aabdef").SetName("Sorting a string using QuickSort"),
		new TestCaseData("sjhfsa", new TreeSort<char>(), "afhjss").SetName("Sorting a string using TreeSort")
	};
}
