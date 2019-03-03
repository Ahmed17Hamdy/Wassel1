using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Wassel.CustomViews
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GreenPin : StackLayout
    {
        private string _display;
        public GreenPin(string display)
        {
            InitializeComponent();
            _display = display;
            BindingContext = this;
        }
        public string Display
        {
            get { return _display; }
        }
    }	
}