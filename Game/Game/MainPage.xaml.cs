using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Game
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
	      InitializeComponent();
          NavigationPage.SetHasNavigationBar(this, false); //schová hroní bar
          //přesměrování po kliknutí na text
          var Redirect = new TapGestureRecognizer();
          Redirect.Tapped += (s, e) =>
          {
            Navigation.PushAsync(new Score(), false);
          };
          scores.GestureRecognizers.Add(Redirect);
          // přesměrování po kliknutí na tlačítko
          play.Clicked += (sender, ea) => {
            
              Navigation.PushAsync(new Intro(), false);
          };
        }
        /// <summary>
        /// anuluje funkci tlačítka zpět
        /// </summary>
        /// <returns></returns>
        protected override bool OnBackButtonPressed()
        {
          return true;
        }
  }
}
