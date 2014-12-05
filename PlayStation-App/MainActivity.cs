using System;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Util;
using PlayStationApp.Adapters;
using PlayStationApp.Core.Entities;
using PlayStationApp.Core.Managers;

namespace PlayStationApp
{
	[Activity (Label = "PlayStation-App", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		int count = 1;
        private UserAccountEntity _userAccountEntity { get; set; }
        protected async override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            var authManager = new AuthenticationManager();
		    var isLoggedIn = authManager.HasLoginTokens();

            if (isLoggedIn)
            {
                _userAccountEntity = new UserAccountEntity();
                isLoggedIn = await LoginTest(_userAccountEntity);
            }
		    if (!isLoggedIn)
		    {
                StartActivity(typeof(LoginActivity));
		        return;
		    }
            var recentActivityManager = new RecentActivityManager();

            var result =
                await
                    recentActivityManager.GetActivityFeed(_userAccountEntity.GetUserEntity().OnlineId, 0, true, true,
                        _userAccountEntity);
            ListView listView = FindViewById<ListView>(Resource.Id.recentActivityList);
            listView.Adapter = new RecentActivityAdapter(this, result.feed);
        }

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


