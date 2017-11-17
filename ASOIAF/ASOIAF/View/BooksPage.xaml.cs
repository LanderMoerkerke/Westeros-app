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
	public partial class BooksPage : ContentPage
	{
		private List<Book> Books = new List<Book>();

		public BooksPage()
		{
			InitializeComponent();
			LoadBooks();

			btnBack.Clicked += BtnBack_Clicked;
		}

		private void BtnBack_Clicked(object sender, EventArgs e)
		{
			Navigation.PopAsync();
		}

		private async void LoadBooks()
		{
			Books = await WesterosManager.GetBooksAsync();
			lvwBooks.ItemsSource = Books;
		}
	}
}