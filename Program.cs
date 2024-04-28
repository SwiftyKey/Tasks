string ProcessTheString(string? text)
{
	if (text == null) throw new ArgumentNullException(nameof(text));

	if (text.Length == 0) return "";

	if (text.Length % 2 == 0)
	{
		var leftPart = text.Substring(0, text.Length / 2);
		var rightPart = text.Substring(text.Length / 2);
		return new string(leftPart.Reverse().ToArray()) + new string(rightPart.Reverse().ToArray());
	}

	return new string(text.Reverse().ToArray()) + text;
}

var inputText = Console.ReadLine();
var processedString = ProcessTheString(inputText);
Console.WriteLine(processedString);
