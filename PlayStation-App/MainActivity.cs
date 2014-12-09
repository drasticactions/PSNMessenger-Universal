using System;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Support.V4.App;
using Android.Support.V7.Widget;
using Android.Util;
using PlayStationApp.Adapters;
using PlayStationApp.Core.Entities;
using PlayStationApp.Core.Managers;
using PlayStationApp.Fragments;

namespace PlayStationApp
{
	[Activity (Label = "PlayStation-App", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		int count = 1;
        private UserAccountEntity UserAccountEntity { get; set; }
        public RecyclerView RecyclerView;
        protected async override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            var authManager = new AuthenticationManager();
		    var isLoggedIn = authManager.HasLoginTokens();

            if (isLoggedIn)
            {
                UserAccountEntity = new UserAccountEntity();
                isLoggedIn = await LoginTest(UserAccountEntity);
            }
		    if (!isLoggedIn)
		    {
		        try
		        {
                    StartActivity(typeof(LoginActivity));
                }
		        catch (Exception ex)
		        {
		            
		            throw;
		        }
		        Finish();
		        return;
		    }
            try
            {
                
                var toolbar = FindViewById<Android.Widget.Toolbar>(Resource.Id.toolbar);
                SetActionBar(toolbar);
                ActionBar.Title = "プレステ API";

            }
            catch (Exception ex)
            {
                
                throw;
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

	    protected override void OnResume()
	    {
	        base.OnResume();
	    }
	}
}


