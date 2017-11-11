using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASOIAF.Model
{
	public class Characters
	{
		[JsonProperty("url")]
		public string Url { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("gender")]
		public string Gender { get; set; }

		[JsonProperty("culture")]
		public string Culture { get; set; }

		[JsonProperty("born")]
		public string Born { get; set; }

		[JsonProperty("died")]
		public string Died { get; set; }

		[JsonProperty("titles")]
		public List<string> Titles { get; set; }

		[JsonProperty("aliases")]
		public List<string> Aliases { get; set; }

		[JsonProperty("father")]
		public string Father { get; set; }

		[JsonProperty("mother")]
		public string Mother { get; set; }

		[JsonProperty("spouse")]
		public string Spouse { get; set; }

		[JsonProperty("allegiances")]
		public List<string> Allegiances { get; set; }

		[JsonProperty("books")]
		public List<string> Books { get; set; }

		[JsonProperty("povBooks")]
		public List<string> PovBooks { get; set; }

		[JsonProperty("tvSeries")]
		public List<string> TvSeries { get; set; }

		[JsonProperty("playedBy")]
		public List<string> PlayedBy { get; set; }
	}
}
