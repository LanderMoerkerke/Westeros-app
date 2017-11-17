using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASOIAF.Model
{
	public class Book
	{
		[JsonProperty("url")]
		public string Url { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("isbn")]
		public string Isbn { get; set; }

		[JsonProperty("authors")]
		public List<string> Authors { get; set; }

		[JsonProperty("numberOfPages")]
		public int NumberOfPages { get; set; }

		[JsonProperty("publisher")]
		public string Publisher { get; set; }

		[JsonProperty("country")]
		public string Country { get; set; }

		[JsonProperty("mediaType")]
		public string MediaType { get; set; }

		[JsonProperty("released")]
		public DateTime Released { get; set; }

		//url of the character
		[JsonProperty("characters")]
		public List<string> Characters { get; set; }

		[JsonProperty("povCharacters")]
		public List<string> PovCharacters { get; set; }

		public int YearReleased
		{
			get { return Released.Year; }
		}

	}
}
