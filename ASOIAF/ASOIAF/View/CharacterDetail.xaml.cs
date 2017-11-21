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

		public CharacterDetail(Character pSelectedCharacter)
		{
			InitializeComponent();

			LoadCharacter(pSelectedCharacter);
		}

		private void LoadCharacter(Character pSelecteredCharacter)
		{
			lblName.Text = pSelecteredCharacter.Name;
			lblGender.Text = pSelecteredCharacter.Gender;
			lblBorn.Text = pSelecteredCharacter.Born;
			lblCulture.Text = pSelecteredCharacter.Culture;

			foreach (string alias in pSelecteredCharacter.Aliases)
			{
				lblAliases.Text += alias + ", ";
			}
			lblAliases.Text = lblAliases.Text.Substring(0, lblAliases.Text.Length - 2);

			if (pSelecteredCharacter.IsAlive)
			{
				lblDiedTip.IsVisible = false;
				lblIsAlive.Text = "Alive";
			}
			else
			{
				lblDiedTip.IsVisible = true;
				lblDied.Text = pSelecteredCharacter.Died;
				lblIsAlive.Text = "Died";
			}

			/*
			 <Label x:Name="lblAliases" TextColor="Black" />
			<Label x:Name="lblAllegiances" TextColor="Black" />
			<Label x:Name="lblGender" TextColor="Black" />
			<Label x:Name="lblCulture" TextColor="Black" />
			<Label x:Name="lblBorn" TextColor="Black" />
			<Label x:Name="lblIsAlive" TextColor="Black" />
			<Label x:Name="lblDied" TextColor="Black" />
			<Label x:Name="lblFather" TextColor="Black" />
			<Label x:Name="lblMother" TextColor="Black" />
			<Label x:Name="lblSpouse" TextColor="Black" />
			 */

		}
	}
}