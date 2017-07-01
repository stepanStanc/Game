using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Game
{
    /// <summary>
    /// Stránka po skončení hry, zobrazí skóre, možnost uložení jména
    /// </summary>
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Outro : ContentPage
	{
		public Outro (Item plr)
		{
			InitializeComponent ();
            NavigationPage.SetHasNavigationBar(this, false);

            //zobrazí skóre
            myScore.Text = "Score: " + plr.Score.ToString();
            
            //klknití na tlačítko
            saveBtn.Command = new Command((() =>
            {
              submitName(plr);
            }));

            //na enter 
            plrName.Completed += (sender, e) => {
              submitName(plr);
            };

            //na dotek - přesměrování na hlavní stranu
            var Redirect = new TapGestureRecognizer();
            Redirect.Tapped += (s, e) =>
            {
              Navigation.PushAsync(new MainPage(), false);
            };
            toMenu.GestureRecognizers.Add(Redirect);
        }              
        /// <summary>
        /// Uložení hráče do dtb
        /// </summary>
        /// <param name="plr"></param>
        private void submitName(Item plr)
        {
          if (!String.IsNullOrEmpty(plrName.Text))
              {
                plr.Name = plrName.Text.Substring(0, Math.Min(20, plrName.Text.Length));
                App.Database.SaveItemAsync(plr);
                Navigation.PushAsync(new MainPage(), false);
              }   
        }

        protected override bool OnBackButtonPressed()
        {
          return true;
        }
    }

    
}
