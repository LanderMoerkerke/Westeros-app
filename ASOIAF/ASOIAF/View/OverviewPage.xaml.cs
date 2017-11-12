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
		public OverviewPage()
		{
			InitializeComponent();

			ShowData();

			btnDiscover.Tapped += BtnDiscover_Tapped;
			btnBooks.Tapped += BtnBooks_Tapped;
			btnCharacters.Tapped += BtnCharacters_Tapped;
			btnHouses.Tapped += BtnHouses_Tapped;
		}

		private void ShowData()
		{
			imgBanner.Source = ImageSource.FromResource("ASOIAF.Image.banner.jpg");
		}

		private void BtnDiscover_Tapped(object sender, EventArgs e)
		{
			Navigation.PushAsync(new )
		}

		private void BtnBooks_Tapped(object sender, EventArgs e)
		{
			throw new NotImplementedException();
		}

		private void BtnCharacters_Tapped(object sender, EventArgs e)
		{
			throw new NotImplementedException();
		}

		private void BtnHouses_Tapped(object sender, EventArgs e)
		{
			throw new NotImplementedException();
		}

		private async void TestGet()
		{
			try
			{
				List<House> collection = await WesterosManager.GetHousesAsync();

				foreach (House item in collection)
				{
					Debug.WriteLine(item.Name);
				}
			}
			catch (Exception e)
			{
				Debug.WriteLine(e.Message);
				throw;
			}
			
		}
	}
}
