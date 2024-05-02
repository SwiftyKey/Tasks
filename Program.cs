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
		OutputString(processedString, "Обработання строка: ");

		var counter = StringHandler.GetCountSymbols(processedString);
		OutputCounter(counter);

		var largestSubstring = StringHandler.GetLargestSubstringStartsAndEndsWithVowel(processedString);
		OutputString(largestSubstring, "Наибольшая подстрока, начинающаяся и заканчивающаяся на гласную: ");

		var sortedProcessedString = new string(sort.Sorting(processedString.ToArray()));
		OutputString(sortedProcessedString, "Отсортированная обработанная строка: ");

		var (shortenedProcessedString, removedCharIndex) = StringHandler.RemoveCharByRandomIndex(processedString);
		OutputString(shortenedProcessedString, $"Обработанная строка с удаленным символом \'{processedString[removedCharIndex]}\' на позиции {removedCharIndex}: ");
	}

	private static ASort<char> GetSortType()
	{
		string? choice;
		do
		{
			var message = "Выберите желаемую сортировку (QuickSort - 0, TreeSort - 1):";
			Console.WriteLine(message);
			choice = Console.ReadLine();
		}
		while (choice != "0" && choice != "1");

		return (choice == "0" ? new QuickSort<char>() : new TreeSort<char>());

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