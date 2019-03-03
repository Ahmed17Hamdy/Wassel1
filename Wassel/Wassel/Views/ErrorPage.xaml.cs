using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wassel.Views.CarsPage;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Wassel.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ErrorPage : ContentPage
	{
        int xx;
		public ErrorPage ()
		{
			InitializeComponent ();
		}
        public ErrorPage(int x)
        {
            InitializeComponent();
            xx = x;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            if(xx==1)
            {
                App.Current.MainPage= new Freightcars();

            }
            else
            {
                if (CrossConnectivity.Current.IsConnected)
                {
                    Navigation.PopAsync();
                }
            }
            
        }
    }
}