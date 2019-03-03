using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wassel.Models;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Forms.Xaml;

namespace Wassel.Views.PopUps
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Carinfo : PopupPage
    {
		public Carinfo (Pin pinselected)
		{
			InitializeComponent ();
        var item= pinselected.Tag as Car;
            var x =  item.id;
		}
	}
}