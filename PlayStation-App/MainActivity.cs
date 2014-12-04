using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Util;
using PlayStationApp.Core.Managers;

namespace PlayStationApp
{
	[Activity (Label = "PlayStation-App", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		int count = 1;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

		    var authManager = new AuthenticationManager();
		    var isLoggedIn = authManager.HasLoginTokens();
		    if (!isLoggedIn)
		    {
                StartActivity(typeof(LoginActivity));
            }
			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			// Get our button from the layout resource,
			// and attach an event to it
			Button button = FindViewById<Button> (Resource.Id.myButton);
			
			button.Click += delegate {
				StartActivity(typeof(LoginActivity));
			};
		}

	    protected override void OnResume()
	    {
	        base.OnResume();
	    }
	}
}


