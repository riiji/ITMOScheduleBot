using ITMOSchedule.Bot.Exceptions;
using ITMOSchedule.Bot.Interfaces;
using VkNet;
using VkNet.Model;

namespace ITMOSchedule.Bot
{
    public class Logger : ILogger
    {
        private readonly VkApi _vkApi;

        public Logger(VkApi vkApi)
        {
            _vkApi = vkApi;
        }

        public void Login()
        {
            _vkApi.Authorize(new ApiAuthParams
            {
                AccessToken = Utilities.BotConfig.AccessKey
            });
        }

        public void Logout()
        {
            if(_vkApi == null)
                throw new BotValidException("Vk api not founded");

            _vkApi.LogOut();
        }
    }
}