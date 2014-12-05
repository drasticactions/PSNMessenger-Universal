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
using PlayStationApp.Adapters;
using PlayStationApp.Core.Entities;
using PlayStationApp.Core.Managers;

namespace PlayStationApp.Fragments
{
    public class RecentActivityFragment : Android.Support.V4.App.Fragment
    {
        private const string TAG = "RecentActivityFragment";
        public RecyclerView recyclerView;
        public RecyclerView.Adapter adapter;
        public RecyclerView.LayoutManager layoutManager;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {

            var rootView = inflater.Inflate(Resource.Layout.recycler_view_frag, container, false);
            rootView.SetTag(rootView.Id, TAG);
            recyclerView = rootView.FindViewById<RecyclerView>(Resource.Id.recyclerView);

            // A LinearLayoutManager is used here, this will layout the elements in a similar fashion
            // to the way ListView would layout elements. The RecyclerView.LayoutManager defines how the
            // elements are laid out.
            layoutManager = new LinearLayoutManager(Activity);
            recyclerView.SetLayoutManager(layoutManager);

            return rootView;
        }


        public async void InitDataSet(UserAccountEntity userAccountEntity)
        {
            var recentActivityManager = new RecentActivityManager();
            var result = await
        recentActivityManager.GetActivityFeed(userAccountEntity.GetUserEntity().OnlineId, 0, true, true,
            userAccountEntity);

            adapter = new RecentActivityRecyclerAdapter(result.feed);
            // Set CustomAdapter as the adapter for RecycleView
            recyclerView.SetAdapter(adapter);
        }
    }
}