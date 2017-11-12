using ASOIAF.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ASOIAF
{
	public partial class OverviewPage : ContentPage
	{
		public OverviewPage()
		{
			InitializeComponent();

			TestGet();
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
