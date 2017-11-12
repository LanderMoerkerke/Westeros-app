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
			int teller = 0;

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

			// kijken of de volgende link verschillend is van de laatste (laatste wordt nog niet verwerkt TO DO)
			while (dictLinks["next"] != dictLinks["last"])
			{
				teller++;

				response = await client.GetAsync(dictLinks["next"]);
				result += response.Content.ReadAsStringAsync();

				headerLink = response.Headers.GetValues("Link").ToList()[0];

				dictLinks["next"] = null;
				dictLinks = ConvertHeaderLinkToDictionairy(headerLink);

				if (teller > 50)
				{
					break;
				}
			}

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
	}
}
