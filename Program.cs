using System.Text;

public class StringHandler
{
	public (string, Dictionary<char, int>?) ProcessTheString(string? text)
	{
		if (text == null) throw new ArgumentNullException(nameof(text));

		if (text.Length == 0 || !InputStringIsCorrect(text)) return ("", null);

		var processedText = new StringBuilder();
		if (text.Length % 2 == 0)
		{
			var leftPart = text.Substring(0, text.Length / 2);
			var rightPart = text.Substring(text.Length / 2);

			processedText.Append(new string(leftPart.Reverse().ToArray()));
			processedText.Append(new string(rightPart.Reverse().ToArray()));
		}
		else
		{
			processedText.Append(new string(text.Reverse().ToArray()));
			processedText.Append(text);
		}
		var processedTextToString = processedText.ToString();
		return (processedTextToString, GetCountSymbols(processedTextToString));
	}

	private bool InputStringIsCorrect(string text)
	{
		var incorrectChars = text.Where(x => (x < 'a' || x > 'z')).ToList();

		if (incorrectChars.Count == 0) return true;

		OutputIncorrectChars(text, incorrectChars);
		return false;
	}

	private void OutputIncorrectChars(string text, List<char> incorrectChars)
	{
		Console.WriteLine($"В введенной строке \'{text}\' имеются некорректные символы: {String.Join(", ", incorrectChars)}");
	}

	private Dictionary<char, int> GetCountSymbols(string text)
	{
		var counter = new Dictionary<char, int>();
		foreach (char c in text.ToHashSet())
			counter[c] = text.Count(x => x == c);

		return counter;
	}
}

internal class MainClass
{
	public static void Main()
	{
		var stringHandler = new StringHandler();
		var inputText = Console.ReadLine();
		var (processedString, counter) = stringHandler.ProcessTheString(inputText);

		OutputProcessedString(processedString);
		OutputCounter(counter);
	}

	private static void OutputProcessedString(string text)
	{
		if (text != "")
			Console.WriteLine("Обработанная строка: " + text);
	}

	private static void OutputCounter(Dictionary<char, int>? counter)
	{
		if (counter != null)
		{
			Console.WriteLine("Количество вхождений каждого символа:");
			foreach (char key in counter.Keys)
				Console.WriteLine($"{key}: {counter[key]}");
		}
	}
}