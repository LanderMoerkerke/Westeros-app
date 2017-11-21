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
			HttpClient client = GetHttpClientForJson();

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
			HttpClient client = GetHttpClientForJson();

			HttpResponseMessage response = await client.GetAsync(new Uri(UrlAll("https://www.anapioficeandfire.com/api/characters")));
			string result = await response.Content.ReadAsStringAsync();

			List<Character> characters = JsonConvert.DeserializeObject<List<Character>>(result);

			// header inlezen, hierin zit de link naar de volgende pagina
			string headerLink = response.Headers.GetValues("Link").ToList()[0];

			// vormt de header om naar een dictionary met de verschillende links naar de 'next', 'first' en 'last' pagina
			Dictionary<string, Uri> dictLinks = ConvertHeaderLinkToLinks(headerLink);

			Dictionary<string, string> dictQueryString = ParseQueryStringFromUri(dictLinks["last"]);

			int lastPage = int.Parse(dictQueryString["page"]);

			List<Task> pageFetchTasks = new List<Task>();

			for (int i = 2; i < lastPage + 1; i++)
			{
				Task<String> fetchPageTask = client.GetStringAsync("https://www.anapioficeandfire.com/api/characters?pageSize=50&page=" + i.ToString());
				pageFetchTasks.Add(fetchPageTask);
			}

			foreach (Task<String> fetchPageTask in pageFetchTasks)
			{
				result = await fetchPageTask;
				characters.AddRange(JsonConvert.DeserializeObject<List<Character>>(result));
			}

			if (result == null)
			{
				return null;
			}

			return characters;
		}

		public static List<Character> FilterListCharactersName(List<Character> pList, string pFiltername)
		{
			return pList.FindAll(c => c.Name.ToLower().Contains(pFiltername.ToLower().Trim()));
		}


		private static HttpClient GetHttpClientForJson() {
			HttpClient client = new HttpClient();
			client.DefaultRequestHeaders.Add("Accept", "application/json");

			return client;
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

		private static Dictionary<string, Uri> ConvertHeaderLinkToLinks(string pHeaderLink)
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
