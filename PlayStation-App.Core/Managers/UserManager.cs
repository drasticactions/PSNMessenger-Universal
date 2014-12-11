using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PlayStationApp.Core.Entities;
using PlayStationApp.Core.Interfaces;
using PlayStationApp.Core.Tools;

namespace PlayStationApp.Core.Managers
{
    public class UserManager
    {
        private readonly IWebManager _webManager;

        public UserManager(IWebManager webManager)
        {
            _webManager = webManager;
        }

        public UserManager()
            : this(new WebManager())
        {
        }

        public async Task<UserEntity> GetUser(string userName, UserAccountEntity userAccountEntity)
        {
            try
            {
                var user = userAccountEntity.GetUserEntity();
                var url = string.Format(EndPoints.User, user.Region, userName);
                var result = await _webManager.GetData(new Uri(url), userAccountEntity);
                var userEntity = JsonConvert.DeserializeObject<UserEntity>(result.ResultJson);
                return userEntity;
            }
            catch (Exception exception)
            {
                throw new Exception("Error getting user", exception);
            }
        }
    }
}
