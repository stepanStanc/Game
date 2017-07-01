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
	public partial class Intro : ContentPage
	{
		public Intro ()
		{
			InitializeComponent ();
            NavigationPage.SetHasNavigationBar(this, false);

            var Redirect = new TapGestureRecognizer();
            Redirect.Tapped += (s, e) =>
            {
              Navigation.PushAsync(new Playground(), false);
            };
            stack.GestureRecognizers.Add(Redirect);
        }
        //lze se vrátit na hlavní stranu
        protected override bool OnBackButtonPressed()
        {
            Navigation.PushAsync(new MainPage(), false);
            return true;
        }
	}
}
