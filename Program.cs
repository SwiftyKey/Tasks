public class StringHandler
{
	public string ProcessTheString(string? text)
	{
		if (text == null) throw new ArgumentNullException(nameof(text));

		if (text.Length == 0 || !InputStringIsCorrect(text)) return "";

		if (text.Length % 2 == 0)
		{
			var leftPart = text.Substring(0, text.Length / 2);
			var rightPart = text.Substring(text.Length / 2);
			return new string(leftPart.Reverse().ToArray()) + new string(rightPart.Reverse().ToArray());
		}

		return new string(text.Reverse().ToArray()) + text;
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
}

internal class MainClass
{
	static public void Main()
	{
		var stringHandler = new StringHandler();
		var inputText = Console.ReadLine();
		var processedString = stringHandler.ProcessTheString(inputText);
		Console.WriteLine(processedString);
	}
}