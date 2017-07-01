using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Game
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Score : ContentPage
	{
		public Score ()
		{
			InitializeComponent ();
            NavigationPage.SetHasNavigationBar(this, false);

            var Redirect = new TapGestureRecognizer();
            Redirect.Tapped += (s, e) =>
            {
              Navigation.PushAsync(new MainPage(), false);
            };
            toMenu.GestureRecognizers.Add(Redirect);

            //načte 10 nejlepšíc score z dtb a vypíše je do listview
            var itemsFromDb = App.Database.GetItemsByScore().Result;
            scoreList.ItemsSource = itemsFromDb;

            //zakáže vybírání itemů v listview
            scoreList.ItemTapped += (object sender, ItemTappedEventArgs e) => {
              if (e.Item == null) return;
              ((ListView)sender).SelectedItem = null; 
            };
        }        
        /// <summary>
        /// tlačítko zpět - hlavní strana
        /// </summary>
        /// <returns></returns>
        protected override bool OnBackButtonPressed()
        {
          Navigation.PushAsync(new MainPage(), false);
          return true;
        }
	}
}
