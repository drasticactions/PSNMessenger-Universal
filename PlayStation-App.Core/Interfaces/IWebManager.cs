using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using PlayStationApp.Core.Entities;
using PlayStationApp.Core.Managers;

namespace PlayStationApp.Core.Interfaces
{
    public interface IWebManager
    {
        bool IsNetworkAvailable { get; }
        Task<WebManager.Result> PostData(Uri uri, FormUrlEncodedContent header, UserAccountEntity userAccountEntity);

        Task<WebManager.Result> GetData(Uri uri, UserAccountEntity userAccountEntity);
    }
}
