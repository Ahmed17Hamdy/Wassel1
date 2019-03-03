using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace Wassel.UWP
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            this.InitializeComponent();
            Xamarin.FormsGoogleMaps.Init("1INpEcE2zqXLPqFMtglJ~CQwn7oNkedOfAseqH-3q4A~AmlVah9TUJi5fNcYhV1Exs8Dwhhw864lQENWrUHL2ah9UOm14qCT56iTfMLDL7bV");

            LoadApplication(new Wassel.App());
        }
    }
}
