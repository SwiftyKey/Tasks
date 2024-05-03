using Microsoft.AspNetCore.Mvc;
using WebTasks.Models;

using Tasks;
using Tasks.Sorts;

namespace WebTasks.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProcessedStringParametersController : ControllerBase
{
	/// <summary>
	/// Если не подходящая строка:
	///		HTTP ошибка 400 Bad Request.Сообщение об ошибке с информацией
	/// Если подходящая строка:
	///		1. Обработанная строка
	///		2. Информация о том, сколько раз входил в обработанную строку каждый символ
	///		3. Самая длинная подстрока начинающаяся и заканчивающаяся на гласную
	///		4. Отсортированная обработанная строка
	///		5. «Урезанная» обработанная строка – обработанная строка без одного символа
	/// </summary>
	/// <param name="text"></param>
	/// <param name="sortType">
	/// <remarks>
	/// sortType приниммет два значения:
	///		0 - QuickSort
	///		1 - TreeSort
	/// </remarks>
	/// </param>
	/// <response code="400">Если text содержит символы отличные от строчных букв английского алфавита</response>
	[HttpGet("{text}/{sortType:int}")]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	public ActionResult<ProcessedStringParameters> Get(string text, int sortType)
	{
		var (processedString, incorrectChars) = StringHandler.ProcessString(text);

		if (incorrectChars?.Count == 1 && StringHandler.BlackList is not null) 
			return BadRequest($"Введенная строка \'{text}\' содержится в черном списке: {String.Join(", ", StringHandler.BlackList)}");
		if (incorrectChars is not null)
			return BadRequest($"В введенной строке \'{text}\' имеются некорректные символы: {String.Join(", ", incorrectChars)}");

		ASort<char> sort;
		switch (sortType)
		{
			case 0:
				sort = new QuickSort<char>();
				break;
			case 1:
				sort = new TreeSort<char>();
				break;
			default:
				return BadRequest("Такой сортировки нет. Выберите из следующего списка: 0 - QuickSort, 1 - TreeSort");
		}

		var shortened = StringHandler.RemoveCharByRandomIndex(processedString);

		return Ok(new ProcessedStringParameters
		{
			SortType = sort.ToString(),
			ProcessedString = processedString,
			Counter = StringHandler.GetCountSymbols(processedString),
			LargestSubstring = StringHandler.GetLargestSubstringStartsAndEndsWithVowel(processedString),
			SortedProcessedString = new string(sort.Sorting(processedString.ToArray())),
			Shortened = new Dictionary<string, object> 
			{
				{ "shortenedProcessedString", shortened.Item1 }, 
				{ "removedCharIndex", shortened.Item2 }
			}
		});
	}
}
