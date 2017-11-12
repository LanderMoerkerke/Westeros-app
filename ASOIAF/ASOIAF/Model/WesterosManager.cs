using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
namespace ASOIAF.Model
{
	public static class WesterosManager
	{
		public static async Task<List<Book>> GetBooksAsync()
		{
			HttpClient client = new HttpClient();
			client.DefaultRequestHeaders.Add("Accept", "application/json");
			
			string result = await client.GetStringAsync("https://www.anapioficeandfire.com/api/books");

			if (result == null)
			{
				return null;
			}

			List<Book> books = JsonConvert.DeserializeObject<List<Book>>(result);
			return books;
		}

		public static async Task<List<Character>> GetCharactersAsync()
		{
			HttpClient client = new HttpClient();
			client.DefaultRequestHeaders.Add("Accept", "application/json");

			string result = await client.GetStringAsync("https://www.anapioficeandfire.com/api/characters");

			if (result == null)
			{
				return null;
			}

			List<Character> characters = JsonConvert.DeserializeObject<List<Character>>(result);
			return characters;
		}

		public static async Task<List<House>> GetHousesAsync()
		{
			HttpClient client = new HttpClient();
			client.DefaultRequestHeaders.Add("Accept", "application/json");

			string result = await client.GetStringAsync("https://www.anapioficeandfire.com/api/houses");

			if (result == null)
			{
				return null;
			}

			List<House> houses = JsonConvert.DeserializeObject<List<House>>(result);
			return houses;
		}
	}
}
