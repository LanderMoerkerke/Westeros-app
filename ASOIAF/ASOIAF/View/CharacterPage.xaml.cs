using ASOIAF.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace ASOIAF.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CharacterPage : ContentPage
	{
		private List<Character> Characters { get; set; }

		public CharacterPage()
		{
			InitializeComponent();
			LoadCharacters();

			lvwCharacters.ItemSelected += LvwCharacters_ItemSelected;
			srchBar.TextChanged += SrchBar_SearchTextChanged;
			btnBack.Clicked += BtnBack_Clicked;
		}

		private void BtnBack_Clicked(object sender, EventArgs e)
		{
			Navigation.PopAsync();
		}

		private void SrchBar_SearchTextChanged(object sender, EventArgs e)
		{
			if (srchBar.Text != string.Empty)
			{
				lvwCharacters.ItemsSource = WesterosManager.FilterListCharactersName(Characters, srchBar.Text);
			}
			else
			{
				lvwCharacters.ItemsSource = Characters;
			}
		}

		private void LvwCharacters_ItemSelected(object sender, SelectedItemChangedEventArgs e)
		{
			if (lvwCharacters.SelectedItem != null)
			{
				Character selectedCharacter = lvwCharacters.SelectedItem as Character;
				Navigation.PushAsync(new CharacterDetail(selectedCharacter));

				lvwCharacters.SelectedItem = null;
			}
		}

		private async void LoadCharacters()
		{
			Characters = await WesterosManager.GetCharactersAsync();
			Characters = Characters.FindAll(c => c.Name != "");

			// Replace all empty Culture
			Characters
		   .Where(x => x.Culture == "")
		   .ToList()
		   .ForEach(x => { x.Culture = "Unidentified"; });

			// Replace all empty Born
			Characters
		   .Where(x => x.Born == "")
		   .ToList()
		   .ForEach(x => { x.Born = "Unmarked"; });

			// Sort list
			Characters = Characters.OrderBy(x => x.Name).ToList();

			lvwCharacters.ItemsSource = Characters;
			CharacterDetail.ListCharacters = Characters;

			ShowContent();
		}

		private void ShowContent()
		{
			stckContent.IsVisible = true;
			//grdContainer.VerticalOptions = LayoutOptions.Start;
			actiActivity.IsVisible = false;
		}
	}
}