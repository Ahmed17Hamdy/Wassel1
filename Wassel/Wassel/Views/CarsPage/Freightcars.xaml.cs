using Plugin.Connectivity;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wassel.CustomViews;
using Wassel.Models;
using Wassel.Services;
using Wassel.ViewModels;
using Wassel.Views.PopUps;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Forms.Xaml;

namespace Wassel.Views.CarsPage
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Freightcars : ContentPage
    {
        private string display;
        CarsViewModel serv;
        public Freightcars()
        {
            InitializeComponent();
            if (CrossConnectivity.Current.IsConnected)
            {
                CarGetter();
                GetLocation();
            }
            else Navigation.PushAsync(new ErrorPage());
        }
        public void GetLocation()
        {

            Position mappos = new Position(24.7253981, 47.3830401);
            MainMap.MoveToRegion(MapSpan.FromCenterAndRadius(mappos, Distance.FromMiles(50)),true);
            
        }
        private ObservableCollection<Car> _cars;

        public ObservableCollection<Car> Cars
        {
            get { return _cars; }
            set { _cars = value; OnPropertyChanged(); }
        }

        public ObservableCollection<Cartype> CarsType { get; set; }
        private void AddAllPin(Car itemcar)
        {
            Label label = new Label();
            Position po = new Position(Convert.ToDouble(itemcar.lat), Convert.ToDouble(itemcar.lng));
            var pin = new Pin
            {
                Type = PinType.Place,
                Position = po,
               
                Label = itemcar.name,
                Address = itemcar.carmodalname,
                Tag = itemcar,
                Icon = (itemcar.type == 1) ? BitmapDescriptorFactory.FromView(new GreenPin(display), null)
                : (itemcar.type == 2) ? BitmapDescriptorFactory.FromView(new RedPin(display), null)
                : BitmapDescriptorFactory.FromView(new MixedPin(display), null)
            };

            MainMap.Pins.Add(pin);

        }

        private async void CarGetter()
        {
            CarServices serv = new CarServices();
            var ResBack = await serv.GetAllCars();
            if(ResBack==null)
            {
                await Navigation.PushAsync(new ErrorPage());
            }
            else
            {
                Cars = ResBack;
                var data = Cars;
                Activ.IsRunning = true;
                foreach (var item in data)
                {
                    AddAllPin(item);
                }
            }
            Activ.IsRunning = false;
        }
        private ObservableCollection<Cartype> _cartypes;

        public ObservableCollection<Cartype> CarTypes
        {
            get
            {
                return _cartypes;
            }
            set
            {
                _cartypes = value;
                OnPropertyChanged();
            }
        }
        private void MyMap_MapClicked(object sender, Xamarin.Forms.GoogleMaps.MapClickedEventArgs e)
        {


            //    Helpers.Settings.LaititudeKeySettings = e.Point.Latitude;
            //   Helpers.Settings.LongitudeKeySettings = e.Point.Latitude;
            //var point = new Position (e.Point.Latitude, e.Point.Longitude);
            //Pin dgragpin = new Pin()
            //{
            //    Label="",
            //    Position = point,
            //    IsDraggable = true,
            //    IsVisible = true
            //};
            //MainMap.Pins.Add(dgragpin);
            //  MainMap.PinDragging += MainMap_PinDragging;
            // MainMap.MapClicked += = false;


        }


        private async void MyMap_PinClickedAsync(object sender, PinClickedEventArgs e)
        {
            var pinselected = e.Pin as Pin;
            var item = pinselected.Tag as Car;
            var pos = pinselected.Position;
         //   carinfo.IsVisible = true;
            ModelFrame.IsVisible = false;
          await PopupNavigation.Instance.PushAsync(new Carinfo(pinselected));
          //  orderbtn.IsVisible = true;
            var point = new Position(pos.Latitude, pos.Longitude);
            Pin dgragpin = new Pin()
            {
                Label = "",
                Position = point,
                IsDraggable = true,
                IsVisible = true
            };
            MainMap.Pins.Add(dgragpin);
          
        }
        private async void MainMap_PinDragEnd(object sender, PinDragEventArgs e)
        {
            var select = e.Pin;
            var drapos = select.Position;
            
            var geocoder = new Xamarin.Forms.GoogleMaps.Geocoder();
            var positions = await geocoder.GetAddressesForPositionAsync(drapos);
            foreach (var place in positions)
            {
                if (positions.Count() > 0)
                {
                    Addresslbl.Text = place ;
                }
                else
                {
                    Addresslbl.Text = "عفواً العنوان غير متوفر";
                }
            }
          //  var place = positions.Last();
//            if (positions.Count() > 0)
//            {
//                var pos = positions.First();
                
//                MainMap.MoveToRegion(MapSpan.FromCenterAndRadius(drapos, Distance.FromMeters(5000)));
//#pragma warning disable CS0618 // Type or member is obsolete
//                var reg = MainMap.VisibleRegion;
//#pragma warning restore CS0618 // Type or member is obsolete
//                var format = "0.00";
//                Addresslbl.Text = $"Center = {reg.Center.Latitude.ToString(format)}, {reg.Center.Longitude.ToString(format)}";
             
//            }
//            else
//            {
//                Addresslbl.Text = "عفواً العنوان غير متوفر";
//                // await this.DisplayAlert("Not found", "Geocoder returns no results", "Close");
//            }
        }
        public void ItemsListView_ItemTappedAsync(object sender, ItemTappedEventArgs e)
        {

            Activ.IsRunning = true;
            if (ModelFrame.IsVisible != true)
            {
                ModelFrame.IsVisible = true;
                ModelActive.IsRunning = true;
            }
            var item = HeaderList.SelectedItem as Cartype;
            var SelectedCars = Cars.Where(o => o.cartype == item.id).ToList();
            foreach (var itemcar in SelectedCars)
            {
                AddAllPin(itemcar);
            }
            Activ.IsRunning = false;

            var carsd = item.carmodals;
            foreach (var modelitem in carsd)
            {
                var ModelIcon = modelitem.icon;
                modelitem.icon = "http://waselksa.alsalil.net/users/images/" + ModelIcon;
            }
            Modellist.ItemsSource = carsd;
            ModelActive.IsRunning = false;
        }
        private void Modellist_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            MainMap.Pins.Clear();
            var ModelSelected = Modellist.SelectedItem as Carmodal;
            var SelectedCars = Cars.Where(o => o.carmodal == ModelSelected.id).ToList();
            foreach (var itemcar in SelectedCars)
            {
                AddAllPin(itemcar);
            }
            Activ.IsRunning = false;

            ModelFrame.IsVisible = false;
        }
        private void AllMap_Tapped(object sender, EventArgs e)
        {
          //  HeaderList.SelectionMode = ListViewSelectionMode.None;
            Activ.IsRunning = true;
            if (ModelFrame.IsVisible!= false)
            {
                ModelFrame.IsVisible = false;
            }           
            MainMap.Pins.Clear();
           // CarGetter();
            foreach (var itemcar in Cars)
            {
                AddAllPin(itemcar);
            }      
            Activ.IsRunning = false;

        }
        //private void Orderbtn_Clicked(object sender, EventArgs e)
        //{
        //    ((Button)sender).IsEnabled = false;
        //    //  ((Map)sender).IsEnabled = false;

        //}

        private void CloseFrame(object sender, EventArgs e)
        {
            ModelFrame.IsVisible = false;
        }

        //private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        //{
        //    Activ.IsRunning = true;
        //       if(ModelFrame.IsVisible!=true)
        //        {
        //           ModelFrame.IsVisible = true;
        //            ModelActive.IsRunning = true;
        //       }
        //    Label stacksender = (Label)sender;
        //    string citem = stacksender.Text;

        //    MainMap.Pins.Clear();
        //    var SelectedCars = Cars.Where(o => o.cartypename == citem).ToList();
        //    foreach (var itemcar in SelectedCars)
        //    {
        //        AddAllPin(itemcar);
        //    }
        //    Activ.IsRunning = false;
        //        var carsd = SelectedCars.ToList();
        //       foreach (var modelitem in carsd)
        //        {
                
        //          var ModelIcon = modelitem.icon;
        //            modelitem.icon = "http://waselksa.alsalil.net/users/images/" + ModelIcon;
        //       }
        //       Modellist.ItemsSource = carsd;
        //        ModelActive.IsRunning = false;
        //}
    }
}