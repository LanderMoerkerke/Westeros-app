using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASOIAF.Model
{
	public class House
	{
		public int HouseId
		{
			get { return int.Parse(Url.Substring(Url.LastIndexOf('/'), Url.Length - Url.LastIndexOf('/'))); }
		}

		[JsonProperty("url")]
		public string Url { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("region")]
		public string Region { get; set; }

		[JsonProperty("coatOfArms")]
		public string CoatOfArms { get; set; }

		[JsonProperty("words")]
		public string Words { get; set; }

		[JsonProperty("titles")]
		public List<string> Titles { get; set; }

		[JsonProperty("seats")]
		public List<string> Seats { get; set; }

		[JsonProperty("currentLord")]
		public string CurrentLord { get; set; }

		[JsonProperty("heir")]
		public string Heir { get; set; }

		[JsonProperty("overlord")]
		public string Overlord { get; set; }

		[JsonProperty("founded")]
		public string Founded { get; set; }

		[JsonProperty("founder")]
		public string Founder { get; set; }

		[JsonProperty("diedOut")]
		public string DiedOut { get; set; }

		[JsonProperty("ancestralWeapons")]
		public List<string> AncestralWeapons { get; set; }

		[JsonProperty("cadetBranches")]
		public List<string> CadetBranches { get; set; }

		[JsonProperty("swornMembers")]
		public List<string> SwornMembers { get; set; }
	}
}
