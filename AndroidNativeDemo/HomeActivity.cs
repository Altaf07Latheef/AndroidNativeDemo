using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Internal;
using Android.Support.Design.Widget;
using Android.Support.V4.Graphics.Drawable;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Microcharts;
using Microcharts.Droid;
using SkiaSharp;
using static Android.Views.View;

namespace AndroidNativeDemo
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class HomeActivity : AppCompatActivity, IOnTouchListener
    {
        #region Properties
        ImageButton dragAbleBt;
        int iniX = 0, iniY = 0;
        int screenWidth = 0;
        int screenHeight = 0;
        int lastX = 0, lastY = 0;
        LinearLayout Linear;
        RecyclerView recyclerView;
        RecyclerView.LayoutManager recyclerview_layoutmanger;
        private RecyclerView.Adapter recyclerview_adapter;
        ScrollView scroll;
        BottomNavigationView bottomNavigation;
        LinearLayout chartstack;
        ChartView chartView;
        private EmployeeListAdapter<Person> EmployeeList = new EmployeeListAdapter<Person>();
        #endregion


        public bool OnTouch(View v, MotionEvent e)
        {
            MotionEventActions ea = e.Action;
            switch (ea)
            {
                case MotionEventActions.Down:
                    v.Parent.RequestDisallowInterceptTouchEvent(true);

                    lastX = (int)e.RawX;
                    lastY = (int)e.RawY;
                    break;
                case MotionEventActions.Move:
                    int dx = (int)e.RawX - lastX;
                    int dy = (int)e.RawY - lastY;
                    int left = v.Left + dx;
                    int right = v.Right + dx;
                    int top = v.Top ;
                    int bottom = v.Bottom ;
                    if (left < 0)
                    {
                        left = 0;
                        right = left + v.Width;
                    }
                    if (right > Linear.Width)
                    {
                        right = Linear.Width;
                        left = right - v.Width;
                    }
                    if (top < 0)
                    {
                        top = 0;
                        bottom = top + v.Height;
                    }
                    if (bottom > Linear.Height)
                    {
                        bottom = Linear.Height;
                        top = bottom - v.Height;
                    }
                    v.Layout(left,top, right, bottom);
                    lastX = (int)e.RawX;
                    lastY = (int)e.RawY;
                    v.PostInvalidate();
                    break;
                case MotionEventActions.Up:

                    dragAbleBt.LayoutParameters = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.WrapContent, ViewGroup.LayoutParams.MatchParent) { Weight = 1 };
                    v.Parent.RequestDisallowInterceptTouchEvent(false);

                    break;
            }

            return false;
        }

        #region Constructor
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.HomePage);

            scroll = FindViewById<ScrollView>(Resource.Id.scrollview);
            Linear = FindViewById<LinearLayout>(Resource.Id.stack);
            screenWidth = Linear.Width;
            screenHeight = Linear.Height;
            dragAbleBt = FindViewById<ImageButton>(Resource.Id.button1);
            dragAbleBt.SetOnTouchListener(this);
            chartstack = FindViewById<LinearLayout>(Resource.Id.chartstack);
            chartView = FindViewById<ChartView>(Resource.Id.chart);
            recyclerView = FindViewById<RecyclerView>(Resource.Id.recyclerview);

            bottomNavigation = FindViewById<BottomNavigationView>(Resource.Id.navigation);
            bottomNavigation.SetShiftMode(false, false);

            //Dummy data for chart
            var entries = new[]
 {
     new Entry(212)
     {
         Label = "12 Jun",
         ValueLabel = "212",
         Color = SKColor.Parse("#CBB268")
     },
     new Entry(248)
     {
         Label = "13 Jun",
         ValueLabel = "248",
         Color = SKColor.Parse("#CBB268")
     },
     new Entry(128)
     {
         Label = "14 Jun",
         ValueLabel = "128",
         Color = SKColor.Parse("#CBB268")
     },
     new Entry(514)
     {
         Label = "15 Jun",
         ValueLabel = "514",
         Color = SKColor.Parse("#CBB268")
},new Entry(128)
     {
         Label = "16 Jun",
         ValueLabel = "128",
         Color = SKColor.Parse("#CBB268")
     },
     new Entry(514)
     {
         Label = "17 Jun",
         ValueLabel = "514",
         Color = SKColor.Parse("#CBB268")
},new Entry(128)
     {
         Label = "18 Jun",
         ValueLabel = "128",
         Color = SKColor.Parse("#CBB268")
     }
};
            PointChart pointChart = new PointChart() { Entries = entries, LabelTextSize = 35, PointSize = 30 };
            chartView.Chart = pointChart;

            //assigning data to recyclerview adapter
            var model = new ViewModel();
            foreach (var s in model.Data)
                EmployeeList.Add(s);

            recyclerview_layoutmanger = new LinearLayoutManager(this, LinearLayoutManager.Horizontal, false);
            recyclerView.SetLayoutManager(recyclerview_layoutmanger);
            recyclerview_adapter = new RecyclerAdapter(EmployeeList);
            recyclerView.SetAdapter(recyclerview_adapter);
        }
        #endregion

        #region Employee data
        public class Person
        {
            public string Name { get; set; }

            public int Image { get; set; }

            public Person()
            {
                Image = Resource.Drawable.userplaceholder;
            }
        }
        public class ViewModel
        {
            public ObservableCollection<Person> Data { get; set; }

            public ViewModel()
            {
                Data = new ObservableCollection<Person>()
            {
                new Person { Name = "David", Image = Resource.Drawable.male },
                new Person { Name = "Michael", Image = Resource.Drawable.female },
                new Person { Name = "Steve" },
                new Person { Name = "Joel", Image = Resource.Drawable.male },
                new Person { Name = "David", Image = Resource.Drawable.male },
                new Person { Name = "Michael" },
                new Person { Name = "Steve", },
                new Person { Name = "Joel", Image = Resource.Drawable.male },
                new Person { Name = "David", Image = Resource.Drawable.male },
                new Person { Name = "Michael", Image = Resource.Drawable.female },
                new Person { Name = "Steve", },
                new Person { Name = "Joel", Image = Resource.Drawable.male },
                new Person { Name = "David" },
                new Person { Name = "Michael", Image = Resource.Drawable.female },
                new Person { Name = "Steve", },
                new Person { Name = "Joel", Image = Resource.Drawable.male }
            };
            }
        }
        #endregion
    }
}