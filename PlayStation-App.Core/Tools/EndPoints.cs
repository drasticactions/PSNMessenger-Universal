using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayStationApp.Core.Tools
{
    public class EndPoints
    {
        public const string ConsumerKey = "4db3729d-4591-457a-807a-1cf01e60c3ac";

        public const string ConsumerSecret = "criemouwIuVoa4iU";

        public const string OauthToken = "https://auth.api.sonyentertainmentnetwork.com/2.0/oauth/token";

        public const string VerifyUser = "https://vl.api.np.km.playstation.net/vl/api/v1/mobile/users/me/info";

        public const string Login =
            "https://auth.api.sonyentertainmentnetwork.com/2.0/oauth/authorize?response_type=code&returnAuthCode=true&service_entity=urn:service-entity:psn&client_id=4db3729d-4591-457a-807a-1cf01e60c3ac&redirect_uri=com.playstation.PlayStationApp://redirect&scope=psn:sceapp";
    }
}
