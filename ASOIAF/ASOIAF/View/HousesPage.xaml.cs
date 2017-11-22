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

		public HousesPage()
		{
			InitializeComponent();
			GetHouses();
			btnBack.Clicked += BtnBack_Clicked;
		}

		private void BtnBack_Clicked(object sender, EventArgs e)
		{
			Navigation.PopAsync();
		}

		private async void GetHouses()
		{
			Houses = await WesterosManager.GetHousesAsync();
			lvwHouses.ItemsSource = Houses;
		}
	}
}