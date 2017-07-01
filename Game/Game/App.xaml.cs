using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Game  //tato aplikace je hra na postřech (nenapadl mně název)
{
	public partial class App : Application
	{

        private static ItemDatabase _database;
        public static ItemDatabase Database
        {
          get
          {
            if (_database == null)
            {
              _database = new ItemDatabase(DependencyService.Get<IFileHelper>().GetLocalFilePath("SQLite.db3"));
            }
            return _database;
          }
        }
        
		public App ()
		{
            MainPage = new NavigationPage(new MainPage());         
            //MainPage = new NavigationPage(new Playground());
        }

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
