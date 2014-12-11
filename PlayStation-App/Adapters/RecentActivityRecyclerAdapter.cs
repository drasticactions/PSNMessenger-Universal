using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Util;
using Android.Views;
using Android.Widget;
using PlayStationApp.Core.Entities;

namespace PlayStationApp.Adapters
{
    public class RecentActivityRecyclerAdapter : RecyclerView.Adapter
    {
        public const string TAG = "RecentActivityRecyclerAdapter";
        private readonly List<RecentActivityEntity.Feed> _items;

        public class ViewHolder : RecyclerView.ViewHolder
        {
            public TextView TextView { get; }
            public ImageView StoreImage { get; }
            public ViewHolder(View v) : base(v)
            {
                TextView = (TextView)v.FindViewById(Resource.Id.textView1);
                StoreImage = (ImageView) v.FindViewById(Resource.Id.storeImage);
            }
        }

        public RecentActivityRecyclerAdapter(List<RecentActivityEntity.Feed> items)
        {
            _items = items;
        }

        public override int GetItemViewType(int position)
        {
            var item = _items[position];
            switch (item.StoryType)
            {
                case "STORE_PROMO":
                    return 1;
                default:
                    return 0;
            }
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder p0, int p1)
        {
            Log.Debug(TAG, "\tElement " + p1 + " set.");

            // Get element from your dataset at this position and replace the contents of the view
            // with that element
            var viewHolder = p0 as ViewHolder;
            viewHolder?.TextView.SetText(_items[p1].Caption, TextView.BufferType.Normal);
            var imageView = viewHolder?.StoreImage;
            if(imageView != null && !string.IsNullOrEmpty(_items[p1].LargeImageUrl))
             Koush.UrlImageViewHelper.SetUrlDrawable(imageView, _items[p1].LargeImageUrl);
            
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup p0, int p1)
        {
            View v = null;
            switch (p1)
            {
                case 1:
                    v = LayoutInflater.From(p0.Context)
    .Inflate(Resource.Layout.StoreActivityView, p0, false);
                    break;
                default:
                    v = LayoutInflater.From(p0.Context)
    .Inflate(Resource.Layout.FriendActivityView, p0, false);
                    break;
            }

            v.Elevation = 5;
            ViewHolder vh = new ViewHolder(v);
            return vh;
        }

        public override int ItemCount => _items.Count;
    }
}