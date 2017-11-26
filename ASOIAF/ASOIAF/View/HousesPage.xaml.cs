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
	public partial class HousesPage : ContentPage
	{
		private List<House> Houses { get; set; }
		private List<Character> Characters;

		public HousesPage()
		{
			InitializeComponent();
			GetHouses();

			btnBack.Clicked += BtnBack_Clicked;
			lvwHouses.ItemSelected += LvwHouses_ItemSelected;
		}

		private  async void GetCharacters()
		{
			Characters = await WesterosManager.GetCharactersAsync();
		}

		private void LvwHouses_ItemSelected(object sender, SelectedItemChangedEventArgs e)
		{
			if (lvwHouses.SelectedItem != null)
			{
				if (Characters != null)
				{
					House selectedHouse = lvwHouses.SelectedItem as House;
					Navigation.PushAsync(new HousesDetail(selectedHouse, Houses, Characters));
					lvwHouses.SelectedItem = null;
				}
				else
				{
					DisplayAlert("Error", "Please wait untill all the data is loaded.", "Ok");
					lvwHouses.SelectedItem = null;
				}
			}
		}

		private void BtnBack_Clicked(object sender, EventArgs e)
		{
			Navigation.PopAsync();
		}

		private async void GetHouses()
		{
			GetCharacters();
			Houses = await WesterosManager.GetHousesAsync();
			lvwHouses.ItemsSource = Houses;
		}
	}
}