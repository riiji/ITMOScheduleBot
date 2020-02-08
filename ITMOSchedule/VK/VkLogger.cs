using System;
using VkLibrary.Core;
using VkLibrary.Core.Auth;
using VkLibrary.Core.Services;
using VkNet;

namespace ITMOSchedule.Vk
{
    public class VkLogger
    {
        private Vkontakte _vkApi;

        public VkLogger(Vkontakte vkApi)
        {
            _vkApi = vkApi;
        }

        public void Login()
        {
            AccessToken accessToken = AccessToken.FromString(VkSettings.Key);

            _vkApi = new Vkontakte(VkSettings.AppId, VkSettings.AppSecret)
            {
                AccessToken = accessToken
            };
        }
    }
}
