using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using Android.Views;
using static Android.Views.View;
using Android.Graphics;

using System.Collections.ObjectModel;
using Microcharts;
using SkiaSharp;
using Microcharts.Droid;
using Android.Support.V7.Widget;
using Java.Lang;
using Android.Support.Design.Widget;
using System;

namespace AndroidNativeDemo
{
    public class MainActivity : AppCompatActivity
    {

        private Android.App.ActionBar toolbar;
        BottomNavigationView bottomNavigation;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

        }
    }
}

