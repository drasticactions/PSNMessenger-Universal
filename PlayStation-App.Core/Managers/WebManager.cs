using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using PlayStationApp.Core.Entities;
using PlayStationApp.Core.Interfaces;

namespace PlayStationApp.Core.Managers
{
    public class WebManager : IWebManager
    {
        public bool IsNetworkAvailable => NetworkInterface.GetIsNetworkAvailable();

        public async Task<Result> PostData(Uri uri, FormUrlEncodedContent header, UserAccountEntity userAccountEntity)
        {
            var httpClient = new HttpClient();
            try
            {
                var authenticationManager = new AuthenticationManager();
                if (userAccountEntity.GetAccessToken().Equals("refresh"))
                {
                    await authenticationManager.RefreshAccessToken(userAccountEntity);
                }
                var user = userAccountEntity.GetUserEntity();
                if (user != null)
                {
                    var language = userAccountEntity.GetUserEntity().Language;
                    httpClient.DefaultRequestHeaders.Add("Accept-Language", language);
                }
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userAccountEntity.GetAccessToken());
                var response = await httpClient.PostAsync(uri, header);
                var responseContent = await response.Content.ReadAsStringAsync();
                return string.IsNullOrEmpty(responseContent) ? new Result(false, string.Empty) : new Result(true, responseContent);
            }
            catch
            {
                // TODO: Add detail error result to json object.
                return new Result(false, string.Empty);
            }
        }

        public async Task<Result> GetData(Uri uri, UserAccountEntity userAccountEntity)
        {
            var httpClient = new HttpClient();
            try
            {
                var authenticationManager = new AuthenticationManager();
                if (userAccountEntity.GetAccessToken().Equals("refresh"))
                {
                    await authenticationManager.RefreshAccessToken(userAccountEntity);
                }
                var user = userAccountEntity.GetUserEntity();
                if (user != null)
                {
                    var language = userAccountEntity.GetUserEntity().Language;
                    httpClient.DefaultRequestHeaders.Add("Accept-Language", language);
                }
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userAccountEntity.GetAccessToken());
                var response = await httpClient.GetAsync(uri);
                var responseContent = await response.Content.ReadAsStringAsync();
                return string.IsNullOrEmpty(responseContent) ? new Result(false, string.Empty) : new Result(true, responseContent);
            }
            catch
            {
                // TODO: Add detail error result to json object.
                return new Result(false, string.Empty);
            }
        }

        public class Result
        {
            public Result(bool isSuccess, string json)
            {
                IsSuccess = isSuccess;
                ResultJson = json;
            }

            public bool IsSuccess { get; private set; }
            public string ResultJson { get; private set; }
        }
    }
}
