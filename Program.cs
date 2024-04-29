using Tasks;
using Tasks.Sorts;

internal class MainClass
{
	public static void Main()
	{
		ASort<char> sort = GetSortType();

		Console.WriteLine("Введите строку:");
		var inputText = Console.ReadLine();

		if (inputText == "")
		{
			Console.WriteLine("Введенная строка пуста");
			return;
		}

		var processedString = StringHandler.ProcessString(inputText);
		var counter = StringHandler.GetCountSymbols(processedString);
		var largestSubstring = StringHandler.GetLargestSubstringStartsAndEndsWithVowel(processedString);
		var sortedProcessedString = new string(sort.Sorting(processedString.ToArray()));

		OutputString(processedString, "Обработання строка: ");
		OutputCounter(counter);
		OutputString(largestSubstring, "Наибольшая подстрока, начинающаяся и заканчивающаяся на гласную: ");
		OutputString(sortedProcessedString, "Отсортированная обработанная строка: ");
	}

	private static ASort<char> GetSortType()
	{
		string choice;
		do
		{
			var message = "Выберите желаемую сортировку (QuickSort - 0, TreeSort - 1):";
			Console.WriteLine(message);
			choice = Console.ReadLine();
		}
		while (choice != "0" && choice != "1");

		return choice switch { "0" => new QuickSort<char>(), "1" => new TreeSort<char>() };

	}

	private static void OutputString(string text, string message)
	{
		if (text != "")
			Console.WriteLine(message + text);
	}

	private static void OutputCounter(Dictionary<char, int> counter)
	{
		if (counter.Keys.Count != 0)
		{
			Console.WriteLine("Количество вхождений каждого символа:");
			foreach (char key in counter.Keys)
				Console.WriteLine($"{key}: {counter[key]}");
		}
	}
}