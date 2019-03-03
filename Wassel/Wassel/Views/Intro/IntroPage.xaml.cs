using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wassel.Views.Panels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Wassel.Views.Intro
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class IntroPage : ContentPage
	{
		public IntroPage ()
		{
			InitializeComponent ();
		}

        private async void UserButton_Cilcked(object sender, EventArgs e)
        {
          await  Navigation.PushAsync(new UserPanel());
        }

        private async void DriverButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DriverPanel());
        }
    }
}