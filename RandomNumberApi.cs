using System.Text.RegularExpressions;

namespace Tasks.RandomNumber
{
	public class RandomNumberApi
	{
		static readonly HttpClient client = new();
		public static string Url { get; set; } = "http://www.randomnumberapi.com/api/v1.0/random?";

		private static async Task<int> GetRandomNumberAsync(string path)
		{
			int randomNumber = -1;

			HttpResponseMessage response = await client.GetAsync(path);
			if (response.IsSuccessStatusCode)
			{
				string result = await response.Content.ReadAsStringAsync();
				Regex rg = new(@"\d+");

				randomNumber = int.Parse(rg.Match(result).Value);
			}

			return randomNumber;
		}

		private static int GenerateRandomNumber(int stringLength) => new Random().Next(stringLength);

		public static int GetRandomNumber(int stringLength)
		{
			var result = GetRandomNumberAsync(Url + $"max={stringLength}").Result;

			if (result != -1) return result;

			return GenerateRandomNumber(stringLength);
		}
	}
}
