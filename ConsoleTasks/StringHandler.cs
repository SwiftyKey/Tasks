using System.Text;
using System.Text.RegularExpressions;
using Tasks.RandomNumber;

namespace Tasks
{
	public static class StringHandler
	{
		public static IEnumerable<string>? BlackList { get; set; }

		public static (string, List<string>?) ProcessString(string? text)
		{
			if (text is null) throw new ArgumentNullException(nameof(text));

			var (isCorrect, incorrectChars) = InputStringIsCorrect(text);

			if (!isCorrect)
				return ("", incorrectChars);

			if (BlackList is not null && BlackList.Any(blackText => blackText == text))
				return ("", new List<string> { text });

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

			return (processedText.ToString(), null);
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

		public static (string, int) RemoveCharByRandomIndex(string text)
		{
			var index = RandomNumberApi.GetRandomNumber(text.Length);
			return (text.Remove(index, 1), index);
		}

		private static (bool, List<string>) InputStringIsCorrect(string text)
		{
			Regex rg = new(@"[^a-z]");
			var incorrectChars = rg.Matches(text).Select(x => x.Value).ToList();

			return (incorrectChars.Count == 0, incorrectChars);
		}

	}
}
