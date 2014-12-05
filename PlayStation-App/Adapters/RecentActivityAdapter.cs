using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using PlayStationApp.Core.Entities;

namespace PlayStationApp.Adapters
{
    public class RecentActivityAdapter : BaseAdapter<RecentActivityEntity.Feed>
    {
        private readonly List<RecentActivityEntity.Feed> _items;
        private readonly Activity _context;

        public RecentActivityAdapter(Activity context, List<RecentActivityEntity.Feed> items) : base() {
            this._context = context;
            this._items = items;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView ?? _context.LayoutInflater.Inflate(Android.Resource.Layout.SimpleListItem1, null); // re-use an existing view, if one is available
            view.FindViewById<TextView>(Android.Resource.Id.Text1).Text = _items[position].Caption;
            return view;
        }

        public override int Count => _items.Count;

        public override RecentActivityEntity.Feed this[int position] => _items[position];
    }
}