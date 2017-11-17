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
		public CharacterDetail(Character pSelectedCharacter)
		{
			InitializeComponent();
		}
	}
}