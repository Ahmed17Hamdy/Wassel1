using System;
using Plugin.CurrentActivity;
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Xamarin.Forms.GoogleMaps.Android;

namespace Wassel.Droid
{
    [Activity(Label = "Wassel", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        readonly string[] permission =
           {
            Android.Manifest.Permission.AccessCheckinProperties,
            Android.Manifest.Permission.AccessFineLocation,
            Android.Manifest.Permission.WriteExternalStorage,
            Android.Manifest.Permission.ReadExternalStorage,
            Android.Manifest.Permission.Internet
            };
        const int RequestId = 0;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            CrossCurrentActivity.Current.Init(this, savedInstanceState);
            Rg.Plugins.Popup.Popup.Init(this, savedInstanceState);
            base.OnCreate(savedInstanceState);
          
            ChechSdk();
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            var platformConfig = new PlatformConfig
            {
                BitmapDescriptorFactory = new CachingNativeBitmapDescriptorFactory()
            };
            Xamarin.FormsGoogleMaps.Init(this, savedInstanceState, platformConfig);
           

            LoadApplication(new App());
        }
        public void ChechSdk()
        {
            if ((int)Build.VERSION.SdkInt > 23)
            {
                RequestPermissions(permission, RequestId);
                return;
            }
            else
            {
                return;
            }
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permission, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

    }
}