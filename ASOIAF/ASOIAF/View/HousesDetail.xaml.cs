using ASOIAF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace ASOIAF.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HousesDetail : ContentPage
	{
		private House SelectedHouse;
		private List<House> Houses;
		private List<Character> Characters;


		public HousesDetail(House pSelectedHouse, List<House> pHouses, List<Character> pCharacters)
		{
			InitializeComponent();

			SelectedHouse = pSelectedHouse;	
			Houses = pHouses;
			Characters = pCharacters;

			ShowData();
			btnBack.Pressed += BtnBack_Pressed;
		}

		private void BtnBack_Pressed(object sender, EventArgs e)
		{
			Navigation.PopAsync();
		}

		private void ShowData()
		{
			imgBackground.Source = ImageSource.FromResource("ASOIAF.Image.castle.jpg");

			List<string> unknown = new List<string>(new string[] { "Unknown" });

			lblName.Text = CheckIfStringEmpty(SelectedHouse.Name);
			
			lblRegion.Text = CheckIfStringEmpty(SelectedHouse.Region);
			lblCoatofArms.Text = CheckIfStringEmpty(SelectedHouse.CoatOfArms);
			lblWords.Text = CheckIfStringEmpty(SelectedHouse.Words);

			if (SelectedHouse.Titles.All(t => string.IsNullOrWhiteSpace(t)))
			{
				FillStackloadWithList(unknown, stckTitles);
			}
			else
			{
				FillStackloadWithList(SelectedHouse.Titles, stckTitles);
			}

			if (SelectedHouse.Seats.All(s => string.IsNullOrWhiteSpace(s)))
			{
				FillStackloadWithList(unknown, stckSeats);
			}
			else
			{
				FillStackloadWithList(SelectedHouse.Seats, stckSeats);
			}

			if (string.IsNullOrWhiteSpace(SelectedHouse.CurrentLord))
			{
				lblLord.Text = "Unknown";
			}
			else
			{
				lblLord.Text = GetCharacterById(GetIdFromUrl(SelectedHouse.CurrentLord), Characters).Name;
			}
			
			if (string.IsNullOrWhiteSpace(SelectedHouse.Heir))
			{
				lblHeir.Text = "Unknown";
			}
			else
			{
				lblHeir.Text = GetCharacterById(GetIdFromUrl(SelectedHouse.Heir), Characters).Name;
			}

			lblFounded.Text = CheckIfStringEmpty(SelectedHouse.Founded);

			if (string.IsNullOrWhiteSpace(SelectedHouse.Founder))
			{
				lblFounder.Text = "Unknown";
			}
			else
			{
				lblFounder.Text = GetCharacterById(GetIdFromUrl(SelectedHouse.Founder), Characters).Name;
			}

			if (SelectedHouse.SwornMembers.All(s => string.IsNullOrWhiteSpace(s)) || SelectedHouse.SwornMembers.Count != 0)
			{
				SelectedHouse.SwornMembers
					.RemoveAll(x => string.IsNullOrWhiteSpace(x));


				SelectedHouse.SwornMembers
					.Select(
						x => new Func<string>(() => { return GetCharacterById(GetIdFromUrl(x), Characters).Name; })
					).Select(t => t.Invoke()).ToArray<string>();
				FillStackloadWithList(unknown, stckMembers);
			}
			else
			{
				FillStackloadWithList(SelectedHouse.SwornMembers, stckMembers);
			}

		}

		private void FillStackloadWithList(List<string> pList, StackLayout pStacklayout)
		{
			foreach (string item in pList)
			{
				pStacklayout.Children.Add(new Label()
				{
					Text = item,
					HorizontalTextAlignment = TextAlignment.End,
					FontSize = 18,
					TextColor = Color.Black
				});
			}
		}

		private string CheckIfStringEmpty(string pString)
		{
			if (string.IsNullOrWhiteSpace(pString))
			{
				return "Unknown";
			}
			else
			{
				return pString;
			}
		}

		private int GetIdFromUrl(string pUrl)
		{
			return int.Parse(pUrl.Substring(pUrl.LastIndexOf('/') + 1, pUrl.Length - pUrl.LastIndexOf('/') - 1));
		}

		private Character GetCharacterById(int pId, List<Character> pList)
		{
			return Characters.Find(character => character.Id == pId);
		}
	}
}