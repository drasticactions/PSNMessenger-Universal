using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PlayStationApp.Core.Entities;
using PlayStationApp.Core.Exceptions;
using PlayStationApp.Core.Helpers;
using PlayStationApp.Core.Interfaces;
using PlayStationApp.Core.Tools;
using Refractored.Xam.Settings;

namespace PlayStationApp.Core.Managers
{
    public class AuthenticationManager
    {
        private readonly IWebManager _webManager;

        public AuthenticationManager(IWebManager webManager)
        {
            _webManager = webManager;
        }

        public AuthenticationManager()
            : this(new WebManager())
        {
        }

        public bool HasLoginTokens()
        {
            return !string.IsNullOrEmpty(CrossSettings.Current.GetValueOrDefault<string>("accessToken"));
        }

        public async Task<UserAccountEntity.User> GetUserEntity(UserAccountEntity userAccountEntity)
        {
            var result = await _webManager.GetData(new Uri(EndPoints.VerifyUser), userAccountEntity);
            try
            {
                var user = UserAccountEntity.ParseUser(result.ResultJson);
                return user;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to parse user", ex);
            }
        }

        public async Task RequestAccessToken(string code)
        {
            try
            {
                var dic = new Dictionary<String, String>();
                dic["grant_type"] = "authorization_code";
                dic["client_id"] = EndPoints.ConsumerKey;
                dic["client_secret"] = EndPoints.ConsumerSecret;
                dic["redirect_uri"] = "com.playstation.PlayStationApp://redirect";
                dic["state"] = "x";
                dic["scope"] = "psn:sceapp";
                dic["code"] = code;
                var theAuthClient = new HttpClient();
                var header = new FormUrlEncodedContent(dic);
                var response = await theAuthClient.PostAsync(EndPoints.OauthToken, header);
                string responseContent = await response.Content.ReadAsStringAsync();
                if (string.IsNullOrEmpty(responseContent))
                {
                    throw new Exception("Failed to get access token");
                }
                var authEntity = new UserAuthenticationEntity();
                authEntity.Parse(responseContent);
                CrossSettings.Current.AddOrUpdateValue("accessToken", authEntity.AccessToken);
                CrossSettings.Current.AddOrUpdateValue("expiresIn", authEntity.ExpiresIn);
                CrossSettings.Current.AddOrUpdateValue("refreshToken", authEntity.RefreshToken);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to get access token", ex); 
            }

        }

        public async Task RefreshAccessToken(UserAccountEntity account)
        {
            try
            {
                var dic = new Dictionary<String, String>();
                dic["grant_type"] = "refresh_token";
                dic["client_id"] = EndPoints.ConsumerKey;
                dic["client_secret"] = EndPoints.ConsumerSecret;
                dic["refresh_token"] = account.GetRefreshToken();
                dic["scope"] = "psn:sceapp";

                account.SetAccessToken("updating", null);
                account.SetRefreshTime(1000);
                var theAuthClient = new HttpClient();
                HttpContent header = new FormUrlEncodedContent(dic);
                var response = await theAuthClient.PostAsync(EndPoints.OauthToken, header);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var o = JObject.Parse(responseContent);
                    if (string.IsNullOrEmpty(responseContent))
                    {
                        throw new RefreshTokenException("Could not refresh the user token, no response data");
                    }
                    account.SetAccessToken((String)o["access_token"], (String)o["refresh_token"]);
                    account.SetRefreshTime(long.Parse((String)o["expires_in"]));

                    var authEntity = new UserAuthenticationEntity();
                    authEntity.Parse(responseContent);
                    CrossSettings.Current.AddOrUpdateValue("accessToken", authEntity.AccessToken);
                    CrossSettings.Current.AddOrUpdateValue("expiresIn", authEntity.ExpiresIn);
                    CrossSettings.Current.AddOrUpdateValue("refreshToken", authEntity.RefreshToken);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Could not refresh the user token", ex);
            }
        }
    }
}
