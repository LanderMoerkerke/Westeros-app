using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ASOIAF.View;
using Xamarin.Forms;

namespace ASOIAF
{
	public partial class App : Application
	{
		public App()
		{
			InitializeComponent();

			MainPage = new NavigationPage(new OverviewPage());
			//MainPage.SetValue(NavigationPage.BarBackgroundColorProperty, Color.Black);
		}

		protected override void OnStart()
		{
			// Handle when your app starts
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}
	}
}
