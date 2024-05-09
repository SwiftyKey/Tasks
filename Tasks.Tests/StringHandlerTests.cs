using FluentAssertions;
using Tasks.Sorts;

namespace Tasks.Tests;

[TestFixture]
public class StringHandlerTests
{
	[TestCase("a", "aa", TestName = "ProcessString_IfStringLengthIsOne_ReturnsDoubledString")]
	[TestCase("", "", TestName = "ProcessString_IfStringIsEmpty_ReturnsEmptyString")]
	[TestCase("ab", "ab", TestName = "ProcessString_IfStringLengthIsTwo_ReturnsSourceString")]
	[TestCase("abcdef", "cbafed", TestName = "ProcessString_IfStringLengthIsEven_ReturnsExpected")]
	[TestCase("abcde", "edcbaabcde", TestName = "ProcessString_IfStringLengthIsOdd_ReturnsExpected")]
	public void ProcessStringTest(string? text, string expected)
	{
		var (resultString, incorrectChars) = StringHandler.ProcessString(text);

		resultString.Should().Be(expected);
		incorrectChars.Should().BeNull();
	}

	[Test]
	public void ProcessString_IfNull_ThrowsArgumentNullException()
	{
		Action action = () => StringHandler.ProcessString(null);

		action.Should().Throw<ArgumentNullException>();
	}

	[TestCaseSource(typeof(StringHandlerTestData), nameof(StringHandlerTestData.IncorrectChars))]
	public void ProcessString_IfStringContainsIncorrectChars_ReturnsIncorrectChars(string? text, List<string> incorrectChars) 
	{
		var result = StringHandler.ProcessString(text).Item2;

		result.Should().BeEquivalentTo(incorrectChars);
	}

	[TestCaseSource(typeof(StringHandlerTestData), nameof(StringHandlerTestData.Counter))]
	public void CounterTest(string text, Dictionary<char, int> counter)
	{
		var processString = StringHandler.ProcessString(text).Item1;
		var result = StringHandler.GetCountSymbols(processString);

		result.Should().BeEquivalentTo(counter);
	}

	[TestCase("bcda", "a", TestName = "ProcessString_IfStringLengthIsEvenWithOneVowel_ReturnsVowelChar")]
	[TestCase("a", "aa", TestName = "ProcessString_IfStringLengthIsOneWithOneVowel_ReturnsDoubledVowelChar")]
	[TestCase("bcdaf", "adcbbcda", TestName = "ProcessString_IfStringLengthIsOddWithOneVowel_ReturnsExpected")]
	[TestCase("abcda", "adcbaabcda", TestName = "ProcessString_IfStringLengthIsEvenWithVowelAtEnds_ReturnsExpected")]
	[TestCase("abybfe", "ybae", TestName = "ProcessString_IfStringLengthIsEven_ReturnsExpected")]
	public void SubstringTest(string text, string expected)
	{
		var processString = StringHandler.ProcessString(text).Item1;
		var result = StringHandler.GetLargestSubstringStartsAndEndsWithVowel(processString);

		result.Should().BeEquivalentTo(expected);
	}

	[TestCaseSource(typeof(StringHandlerTestData), nameof(StringHandlerTestData.Sorts))]
	public void SortTest(string text, ASort<char> sort, string expected)
	{
		var processString = StringHandler.ProcessString(text).Item1;
		var result = new string(sort.Sorting(processString.ToArray()));

		result.Should().BeEquivalentTo(expected);
	}
}