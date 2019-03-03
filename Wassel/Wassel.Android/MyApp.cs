using System;
using Android.App;
using Android.Runtime;

namespace Wassel.Droid
{
    [Application]
    [MetaData("com.google.android.maps.v2.API_KEY",
              Value = "AIzaSyD0vVJKAQz1OTMTxzvXEEyFFtiEKlpLwZI")]
    public class MyApp : Application
    {
        public MyApp(IntPtr javaReference, JniHandleOwnership transfer)
            : base(javaReference, transfer)
        {
        }
    }
}