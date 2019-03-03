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
    public partial class RedPin : StackLayout
    {
        private string _display;
        public RedPin(string display)
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