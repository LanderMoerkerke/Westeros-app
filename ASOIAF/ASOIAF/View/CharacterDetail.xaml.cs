using ASOIAF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ASOIAF.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CharacterDetail : ContentPage
	{
		public static List<Character> ListCharacters { get; set; }
		private static List<House> ListHouses;

		public CharacterDetail(Character pSelectedCharacter)
		{
			InitializeComponent();
			LoadCharacter(pSelectedCharacter);
			btnBack.Pressed += BtnBack_Pressed;
		}

		private void BtnBack_Pressed(object sender, EventArgs e)
		{
			Navigation.PopAsync();
		}

		private void LoadCharacter(Character pSelecteredCharacter)
		{
			lblName.Text = pSelecteredCharacter.Name;

			List<string> aliases = pSelecteredCharacter.Aliases.Where(s => !string.IsNullOrWhiteSpace(s)).Distinct().ToList();
			if (aliases.Count != 0)
			{
				StackLayout stack = stckAliases;

				foreach (string alias in aliases)
				{
					Label lblAlias = new Label()
					{
						Text = alias,
						TextColor = Color.Black ,
						FontSize = 18
					};
					stack.Children.Add(lblAlias);
				}
			}
			else
			{
				grdAliases.IsVisible = false;
			}

			//if (pSelecteredCharacter.Allegiances.Count != 0)
			//{
			//	foreach (string alias in pSelecteredCharacter.Allegiances)
			//	{
			//		//House house = await GetHouseNameById(alias, ListHouses);
			//		//lblAllegiances.Text += await GetHouseNameById(alias, ListHouses) + ", ";

			//	}
			//	//lblAllegiances.Text = lblAllegiances.Text.Substring(0, lblAllegiances.Text.Length - 2);
			//}
			//else
			//{
			//	grdAlegiances.IsVisible = false;
			//}

			grdAlegiances.IsVisible = false;


			if (pSelecteredCharacter.Gender != string.Empty)
			{
				lblGender.Text = pSelecteredCharacter.Gender;
			}
			else
			{
				grdGender.IsVisible = false;
			}

			if (pSelecteredCharacter.Culture != string.Empty)
			{
				lblCulture.Text = pSelecteredCharacter.Culture;
			}
			else
			{
				grdCulture.IsVisible = false;
			}

			if (pSelecteredCharacter.Born != string.Empty)
			{
				lblBorn.Text = pSelecteredCharacter.Born;
			}
			else
			{
				grdBorn.IsVisible = false;
			}

			if (pSelecteredCharacter.IsAlive)
			{
				grdDiedTip.IsVisible = false;
				lblIsAlive.Text = "Alive";
			}
			else
			{
				lblDied.Text = pSelecteredCharacter.Died;
				lblIsAlive.Text = "Died";
			}

			List<string> familyTree = new List<string>(new string[] { pSelecteredCharacter.Father, pSelecteredCharacter.Mother, pSelecteredCharacter.Spouse });
			familyTree = familyTree.Where(s => !string.IsNullOrWhiteSpace(s)).Distinct().ToList();

			if (familyTree.Count != 0)
			{
				if (pSelecteredCharacter.Father != string.Empty)
				{
					lblFather.Text = GetCharacterById(GetIdFromUrl(pSelecteredCharacter.Father), ListCharacters).Name;
				}
				else
				{
					lblFather.Text = "Unknown";
				}

				if (pSelecteredCharacter.Mother != string.Empty)
				{
					lblMother.Text = GetCharacterById(GetIdFromUrl(pSelecteredCharacter.Mother), ListCharacters).Name;
				}
				else
				{
					lblMother.Text = "Unknown";
				}

				if (pSelecteredCharacter.Spouse != string.Empty)
				{
					lblSpouse.Text = GetCharacterById(GetIdFromUrl(pSelecteredCharacter.Spouse), ListCharacters).Name;
				}
				else
				{
					lblSpouse.Text = "Unknown";
				}
			}
			else
			{
				grdFamilyTree.IsVisible = false;
			}
		}

		private int GetIdFromUrl(string pUrl)
		{
			return int.Parse(pUrl.Substring(pUrl.LastIndexOf('/') + 1, pUrl.Length - pUrl.LastIndexOf('/') - 1));
		}

		private Character GetCharacterById(int pId, List<Character> pList)
		{
			return ListCharacters.Find(character => character.Id == pId);
		}
		private async Task<string> GetHouseNameById(string pUrl, List<House> pList)
		{
			await GetHouses();
			House house = ListHouses.Find(h => h.Url == pUrl);
			return house.Name;
		}

		private async Task GetHouses()
		{
			if (ListHouses != null)
			{
				ListHouses = await WesterosManager.GetHousesAsync();
			}
		}
	}
}