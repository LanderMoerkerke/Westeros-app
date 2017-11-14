using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace ASOIAF.Model
{
	public static class WesterosManager
	{
		private static int PageLimit = 50;

		private static string UrlAll(string pUrl)
		{
			return pUrl + "?pageSize=" + PageLimit.ToString();
		}

		public static async Task<List<Book>> GetBooksAsync()
		{
			HttpClient client = new HttpClient();
			client.DefaultRequestHeaders.Add("Accept", "application/json");

			HttpResponseMessage response = await client.GetAsync(new Uri(UrlAll("https://www.anapioficeandfire.com/api/books")));
			string result = await response.Content.ReadAsStringAsync();


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

			HttpResponseMessage response = await client.GetAsync(new Uri(UrlAll("https://www.anapioficeandfire.com/api/characters")));

			string result = await response.Content.ReadAsStringAsync();

			//"<https://www.anapioficeandfire.com/api/books?page=2&pageSize=10>; rel=\"next\", <https://www.anapioficeandfire.com/api/books?page=1&pageSize=10>; rel=\"first\", <https://www.anapioficeandfire.com/api/books?page=2&pageSize=10>; rel=\"last\""
			//bool isUri = Uri.IsWellFormedUriString(nextUrl, UriKind.RelativeOrAbsolute);

			// header inlezen, hierin zit de link naar de volgende pagina
			string headerLink = response.Headers.GetValues("Link").ToList()[0];

			// vormt de header om naar een dictionary met de verschillende links naar de 'next', 'first' en 'last' pagina
			Dictionary<string, Uri> dictLinks = ConvertHeaderLinkToDictionairy(headerLink);

			Dictionary<string, string> dictQueryString = ParseQueryStringFromUri(dictLinks["last"]);

			int lastPage = int.Parse(dictQueryString["page"]);

			// tests
			string url = "https://www.anapioficeandfire.com/api/characters?pageSize=50&page=";

			string result2 = string.Empty;
			List<Task> tasks = new List<Task>();

			List<Character> characters = JsonConvert.DeserializeObject<List<Character>>(result);
			for (int i = 2; i < lastPage + 1; i++)
			{
				//string getUrl = url + i.ToString();
				//result += client.GetStringAsync(getUrl);
				//Task task = Task.Run(() => result2 += client.GetStringAsync(getUrl));
				//tasks.Add(task);

				HttpResponseMessage response2 = await client.GetAsync(UrlAll("https://www.anapioficeandfire.com/api/characters") + "&page=" + i.ToString());
				result2 = await response.Content.ReadAsStringAsync();

				List<Character> characters2 =  JsonConvert.DeserializeObject<List<Character>>(result2);
				characters.AddRange(characters2);
			}
			//await Task.WhenAll(tasks);
			


			if (result == null)
			{
				return null;
			}

			try
			{
				return characters;
			}
			catch (Exception e)
			{
				Debug.WriteLine(e.Message);
				throw;
			}
		}

		public static async Task<List<House>> GetHousesAsync()
		{
			HttpClient client = new HttpClient();
			client.DefaultRequestHeaders.Add("Accept", "application/json");

			string result = await client.GetStringAsync(new Uri(UrlAll("https://www.anapioficeandfire.com/api/houses")));

			if (result == null)
			{
				return null;
			}

			List<House> houses = JsonConvert.DeserializeObject<List<House>>(result);
			return houses;
		}

		private static Dictionary<string, Uri> ConvertHeaderLinkToDictionairy(string pHeaderLink)
		{
			Dictionary<string, Uri> dictLinks = new Dictionary<string, Uri>();

			Regex regx = new Regex("(ftp|https?):[^\\s>]+", RegexOptions.IgnoreCase);
			MatchCollection ms = regx.Matches(pHeaderLink);

			switch (ms.Count)
			{
				case 2:
					dictLinks.Add("first", new Uri(ms[0].ToString()));
					dictLinks.Add("last", new Uri(ms[1].ToString()));
					break;
				case 3:
					dictLinks.Add("next", new Uri(ms[0].ToString()));
					dictLinks.Add("first", new Uri(ms[1].ToString()));
					dictLinks.Add("last", new Uri(ms[2].ToString()));
					break;
				default:
					break;
			}

			return dictLinks;
		}

		private static Dictionary<string, string> ParseQueryStringFromString(string uri)
		{
			string substring = uri.Substring(((uri.LastIndexOf('?') == -1) ? 0 : uri.LastIndexOf('?') + 1));

			string[] pairs = substring.Split('&');

			Dictionary<string, string> output = new Dictionary<string, string>();

			foreach (string piece in pairs)
			{
				string[] pair = piece.Split('=');
				output.Add(pair[0], pair[1]);
			}
			return output;
		}
		private static Dictionary<string, string> ParseQueryStringFromUri(Uri uri)
		{
			return ParseQueryStringFromString(uri.ToString());
		}
	}
}
