using ASOIAF.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ASOIAF.View
{
	public partial class OverviewPage : ContentPage
	{
		public List<Character> Characters { get; set; }

		public OverviewPage()
		{
			InitializeComponent();

			ShowData();

			btnDiscover.Tapped += BtnDiscover_Tapped;
			btnBooks.Tapped += BtnBooks_Tapped;
			btnCharacters.Tapped += BtnCharacters_Tapped;
			btnHouses.Tapped += BtnHouses_Tapped;

			TestGet();
		}

		private void ShowData()
		{
			imgBanner.Source = ImageSource.FromResource("ASOIAF.Image.banner.jpg");
		}

		private void BtnDiscover_Tapped(object sender, EventArgs e)
		{
			throw new NotImplementedException();
		}

		private void BtnBooks_Tapped(object sender, EventArgs e)
		{
			Navigation.PushAsync(new BooksPage());
		}

		private void BtnCharacters_Tapped(object sender, EventArgs e)
		{
			Navigation.PushAsync(new CharacterPage());
		}

		private void BtnHouses_Tapped(object sender, EventArgs e)
		{
			Navigation.PushAsync(new HousesPage());
		}

		private async void TestGet()
		{
			Characters = await WesterosManager.GetCharactersAsync();
		}
	}
}
