using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PlayStationApp.Core.Entities;
using PlayStationApp.Core.Interfaces;

namespace PlayStationApp.Core.Managers
{
    public class RecentActivityManager
    {
        private readonly IWebManager _webManager;

        public RecentActivityManager(IWebManager webManager)
        {
            _webManager = webManager;
        }

        public RecentActivityManager()
            : this(new WebManager())
        {
        }

        public async Task<RecentActivityEntity> GetActivityFeed(string userName, int? pageNumber, bool storePromo,
            bool isNews, UserAccountEntity userAccountEntity)
        {
            try
            {
                var feedNews = isNews ? "news" : "feed";
                string url = string.Format("https://activity.api.np.km.playstation.net/activity/api/v1/users/{0}/{1}/{2}?filters=PURCHASED&filters=RATED&filters=VIDEO_UPLOAD&filters=SCREENSHOT_UPLOAD&filters=PLAYED_GAME&filters=WATCHED_VIDEO&filters=TROPHY&filters=BROADCASTING&filters=LIKED&filters=PROFILE_PIC&filters=FRIENDED&filters=CONTENT_SHARE", userName, feedNews, pageNumber);
                if (storePromo)
                    url += "&filters=STORE_PROMO";
                url += "&r=" + Guid.NewGuid();
                var result = await _webManager.GetData(new Uri(url), userAccountEntity);
                var recentActivityEntity = JsonConvert.DeserializeObject<RecentActivityEntity>(result.ResultJson);
                return recentActivityEntity;
            }
            catch (Exception exception)
            {
                
                throw new Exception("Error getting activity feed", exception);
            }
        }
    }
}
