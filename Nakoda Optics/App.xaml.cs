using System;
using System.Security.Cryptography.X509Certificates;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Nakoda_Optics
{
    public partial class App : Application
    {
        public static string LocalDatabase = string.Empty;
        public App(string database)
        {
            InitializeComponent();
            LocalDatabase = database;
            MainPage = new NavigationPage (new MainPage() );
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
