using System;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Support.V4.App;
using Android.Support.V4.View;
using Android.Support.V4.Widget;
using Android.Support.V7.Widget;
using Android.Util;
using PlayStationApp.Adapters;
using PlayStationApp.Core.Entities;
using PlayStationApp.Core.Managers;
using PlayStationApp.Fragments;

namespace PlayStationApp
{
    [Activity (Label = "PlayStation-App", MainLauncher = true, Icon = "@drawable/icon", ConfigurationChanges = Android.Content.PM.ConfigChanges.Orientation | Android.Content.PM.ConfigChanges.ScreenSize)]
	public class MainActivity : FragmentActivity
	{
		int count = 1;
        private UserAccountEntity UserAccountEntity { get; set; }
        public RecyclerView RecyclerView;
        private DrawerLayout mDrawerLayout;
        private RecyclerView mDrawerList;
        private ActionBarDrawerToggle mDrawerToggle;
        private string mDrawerTitle;

        protected async override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

            // Set our view from the "main" layout resource

      //      var authManager = new AuthenticationManager();
		    //var isLoggedIn = authManager.HasLoginTokens();
      //      if (isLoggedIn)
      //      {
      //          UserAccountEntity = new UserAccountEntity();
      //          isLoggedIn = await LoginTest(UserAccountEntity);
      //      }
		    //if (!isLoggedIn)
		    //{
      //          StartActivity(typeof(LoginActivity));
      //          Finish();
		    //    return;
		    //}

            SetContentView(Resource.Layout.Main);

            //var toolbar = FindViewById<Android.Widget.Toolbar>(Resource.Id.toolbar);
            //SetActionBar(toolbar);
            //this.ActionBar.SetDisplayHomeAsUpEnabled(true);
            //ActionBar.SetHomeButtonEnabled(true);
            //ActionBar.Title = "プレステ API";


            //var transaction = SupportFragmentManager.BeginTransaction();
            //var fragment = new RecentActivityFragment(UserAccountEntity);
            //transaction.Replace(Resource.Id.recentActivityFragment, fragment);
            //transaction.Commit();
        }
        internal class MyActionBarDrawerToggle : ActionBarDrawerToggle
        {
            MainActivity owner;

            public MyActionBarDrawerToggle(MainActivity activity, DrawerLayout layout, int imgRes, int openRes, int closeRes)
                : base(activity, layout, imgRes, openRes, closeRes)
            {
                owner = activity;
            }

            public override void OnDrawerClosed(View drawerView)
            {
                owner.ActionBar.Title = owner.Title;
                owner.InvalidateOptionsMenu();
            }

            public override void OnDrawerOpened(View drawerView)
            {
                owner.ActionBar.Title = owner.mDrawerTitle;
                owner.InvalidateOptionsMenu();
            }
        }
        public RecyclerView.Adapter adapter;

        private async Task<bool> LoginTest(UserAccountEntity userAccountEntity)
	    {
            var authManager = new AuthenticationManager();
            UserAccountEntity.User user = await authManager.GetUserEntity(userAccountEntity);
            if (user == null) return false;
            userAccountEntity.SetUserEntity(user);
	        return true;
	    }
	}
}


