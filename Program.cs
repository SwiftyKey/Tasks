using System.Text;
using System.Text.RegularExpressions;

public class StringHandler
{
	public static string ProcessString(string? text)
	{
		if (text == null) throw new ArgumentNullException(nameof(text));

		if (text.Length == 0 || !InputStringIsCorrect(text)) return "";

		var processedText = new StringBuilder();
		if (text.Length % 2 == 0)
		{
			var leftPart = text[..(text.Length / 2)];
			var rightPart = text[(text.Length / 2)..];

			processedText.Append(new string(leftPart.Reverse().ToArray()));
			processedText.Append(new string(rightPart.Reverse().ToArray()));
		}
		else
		{
			processedText.Append(new string(text.Reverse().ToArray()));
			processedText.Append(text);
		}

		return processedText.ToString();
	}

	public static Dictionary<char, int> GetCountSymbols(string text)
	{
		var counter = new Dictionary<char, int>();
		foreach (char c in text.ToHashSet())
			counter[c] = text.Count(x => x == c);

		return counter;
	}

	public static string GetLargestSubstringStartsAndEndsWithVowel(string text)
	{
		Regex rg = new(@"[aeiouy][a-z]*[aeiouy]|[aeiouy]");
		return rg.Match(text).Value;
	}

	private static bool InputStringIsCorrect(string text)
	{
		Regex rg = new(@"[^a-z]");
		var incorrectChars = rg.Matches(text).Select(x => x.Value).ToList();

		if (incorrectChars.Count == 0) return true;

		OutputIncorrectChars(text, incorrectChars);
		return false;
	}

	private static void OutputIncorrectChars(string text, List<string> incorrectChars)
	{
		Console.WriteLine($"В введенной строке \'{text}\' имеются некорректные символы: {String.Join(", ", incorrectChars)}");
	}
}

internal class MainClass
{
	public static void Main()
	{
		var inputText = Console.ReadLine();
		var processedString = StringHandler.ProcessString(inputText);
		var counter = StringHandler.GetCountSymbols(processedString);
		var largestSubstring = StringHandler.GetLargestSubstringStartsAndEndsWithVowel(processedString);

		OutputString(processedString, "Обработання строка: ");
		OutputCounter(counter);
		OutputString(largestSubstring, "Наибольшая подстрока, начинающаяся и заканчивающаяся на гласную: ");
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