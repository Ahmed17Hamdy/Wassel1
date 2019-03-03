using Plugin.Connectivity;
using System;
using Wassel.Views;
using Wassel.Views.CarsPage;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Wassel
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            if (CrossConnectivity.Current.IsConnected)
            {
                MainPage = new Freightcars();
            }
            else
            {
                MainPage = new NavigationPage(new ErrorPage(1));
            }
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
