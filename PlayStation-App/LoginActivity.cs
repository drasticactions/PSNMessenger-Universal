using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Net.Http;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Webkit;
using Android.Widget;
using PlayStationApp.Core.Managers;
using PlayStationApp.Core.Tools;
using PlayStationApp.Core.Tools.UriExtensions;

namespace PlayStationApp
{
    [Activity(Label = "LoginActivity")]
    public class LoginActivity : Activity
    {
        public WebView _webView;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Login);
            _webView = FindViewById<WebView>(Resource.Id.webView1);
            _webView.Settings.JavaScriptEnabled = true;
            _webView.SetWebViewClient(new Client(this));
            _webView.LoadUrl(EndPoints.Login);
        }

        class Client : WebViewClient
        {
            private LoginActivity _activity;

            public Client(LoginActivity activity)
            {
                _activity = activity;
            }

            public async override void OnPageStarted(WebView view, string url, Android.Graphics.Bitmap favicon)
            {
                // TODO: Add loading bar, make it less shitty. This is just a test...
                if (!url.ToLower().StartsWith("com.playstation.playstationapp")) return;

                _activity._webView.Visibility = ViewStates.Invisible;
                var queryString = ParseQueryString(url);
                if (!queryString.ContainsKey("authCode")) return;
                var authManager = new AuthenticationManager();
                await authManager.RequestAccessToken(queryString["authCode"]);
            }
        }
    }
}