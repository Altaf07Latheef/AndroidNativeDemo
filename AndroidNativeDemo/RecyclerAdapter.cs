using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;

namespace AndroidNativeDemo
{
    public class RecyclerAdapter : RecyclerView.Adapter
    {
        private EmployeeListAdapter<HomeActivity.Person> Mitems;
        Context context;
        public RecyclerAdapter(EmployeeListAdapter<HomeActivity.Person> Mitems)
        {
            this.Mitems = Mitems;
            NotifyDataSetChanged();
        }
        public class MyView : RecyclerView.ViewHolder
        {
            public View mainview
            {
                get;
                set;
            }
            public ImageView mtxtcontactname
            {
                get;
                set;
            }
            public TextView mtxtcontactnumber
            {
                get;
                set;
            }
            public MyView(View view) : base(view)
            {
                mainview = view;
            }
        }
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View listitem = LayoutInflater.From(parent.Context).Inflate(Resource.Drawable.EmployeeLayout, parent, false);
            ImageView txtcontactname = listitem.FindViewById<ImageView>(Resource.Id.txtcontactname);
            TextView txtnumber = listitem.FindViewById<TextView>(Resource.Id.txtnumber);
            MyView view = new MyView(listitem)
            {
                mtxtcontactname = txtcontactname,
                mtxtcontactnumber = txtnumber
            };
            return view;
        }
        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            MyView myholder = holder as MyView;
            myholder.mtxtcontactname.SetImageResource(Mitems[position].Image);
            myholder.mtxtcontactnumber.Text = Mitems[position].Name;
        }
        public override int ItemCount
        {
            get
            {
                return Mitems.Count;
            }
        }
    }
}