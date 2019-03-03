
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Wassel.Model;
using Wassel.Models;
using Wassel.Services;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Wassel.ViewModel
{
 public   class RegisterViewModel : INotifyPropertyChanged
    {
        public Cartype cartype { get; set; }
        
        public RegisterViewModel()
        {
      //    AddToCarTypeList();
        }
        
        UserService userService = new UserService();
        public int id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string carnumber { get; set; }
        public string carmodal { get; set; }
        public string country { get; set; }
        public string city { get; set; }
        public string phone { get; set; }
        public string age { get; set; }
        public string national { get; set; }
        public string image { get; set; }
        public string idnumber { get; set; }
        public string denominationnumber { get; set; }
        public string passportnumber { get; set; }
        public string nationality { get; set; }
        public string type { get; set; }
        public string password { get; set; }
        public string confirmpass { get; set; }
        public string firebase_token { get; set; }
        public string device_id { get; set; }
        public string lat { get; set; }

        public StreamImageSource Img { get; set; }
        public string lng { get; set; }
        
     
        public ICommand UserRegisterCommand => new Command(async () =>
        {
            var location = await Geolocation.GetLocationAsync();
            var device = DeviceInfo.Model;
            User _userReg = new User
            {
                email = email,
                name = name,
                password = password,
                confirmpass = confirmpass,
                firebase_token = "35",
                device_id = "111.2225.555",
                lat = location.Latitude.ToString(),
                lng = location.Longitude.ToString()

            };
            var ResBack = await userService.RegisterAsync(_userReg);
            bool checker = false;
            if (ResBack == "false")
            {
                PopAlert(checker);
            }
            else
            {

                try
                {

                    var JsonResponse = JsonConvert.DeserializeObject<Response<string, DriverUser>>(ResBack);
                    if (JsonResponse.success == true)
                    {
                        //  DisplayAlert("Alert", "You have been alerted", "OK");
                        checker = true;
                        PopAlert(checker);
                        Device.BeginInvokeOnMainThread(() => App.Current.MainPage = new MainPage());
                    }
                    else
                    {

                        return;
                    }
                }
                catch (Exception)
                {

                    var JsonResponse = JsonConvert.DeserializeObject<Response<object, string>>(ResBack);

                    return;
                }
            }

        });
        public ICommand LoginCommand => new Command(async () =>
        {
            name = name;
            email = email;
            password = password;
            confirmpass = confirmpass;
            firebase_token = "0";
            device_id = "0";
            var ResBack = await userService.LoginCommandAsync(name, email, password, confirmpass, firebase_token, device_id);

           // email = Settings.LastUsedEmail;
            if (ResBack == null)
            {
              //  Activ.IsRunning = false;
               // await DisplayAlert("Communication Error", "من فضلك تحقق من الإتصال بالإنترنت", "OK");
            }
            else
            {
                bool checker = false;
                try
                {
                   
                    var JsonResponse = JsonConvert.DeserializeObject<Response<string, DriverUser>>(ResBack);
                    if (JsonResponse.success == true)
                    {
                        var _userID = JsonResponse.message.id;
                        checker = true;
                       // Settings.LastUsedID = _userID;
                      //  Settings.LastUsedEmail = EntryEmail.Text;
                        PopAlert(checker);
                        Device.BeginInvokeOnMainThread(() => App.Current.MainPage = new MainPage());
                    }
                    else
                    {
                       
                        PopAlert(checker);
                        return;
                    }
                }
                catch (Exception)
                {
                    
                    var JsonResponse = JsonConvert.DeserializeObject<Response<object, string>>(ResBack);
                    PopAlert(checker);
                    return;
                }
            }

        });
        private ObservableCollection<Cartype> _carstypelist = new ObservableCollection<Cartype>();
        public ObservableCollection<Cartype> CarsTypeList
        {
            get
            {
                return _carstypelist;
            }
            set
            {
                _carstypelist = value;
                OnPropertyChanged(nameof (CarsTypeList));
            }
        }

        public List<string> TypeCarName { get; private set; }

        private async void AddToCarTypeList (params Cartype []  cartypes )
        {
            UserService CarType = new UserService();
            
            {
                CarsTypeList = await CarType.GetCarstype();
               
            }
            
            foreach (Cartype typecar in cartypes)
            {
               
               // CarsTypeList.Add(typecar);
              // TypeCarName = CarsTypeList.Select(o => o.name).ToList();
            }
            
            CarsTypeList = CarsTypeList;
        }
      

        private void PopAlert(bool x)
        {
        //    PopupNavigation.Instance.PushAsync(new TrueRegister());
            return;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
