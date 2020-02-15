using System;
using ITMOSchedule.Bot;
using ITMOSchedule.Bot.Exceptions;
using ITMOSchedule.Bot.Interfaces;
using VkApi.Wrapper;
using VkApi.Wrapper.Auth;
using VkApi.Wrapper.LongPolling.Bot;
using VkApi.Wrapper.Types.Groups;
using VkApi.Wrapper.Types.Messages;

namespace ITMOSchedule.VK
{
    public class VkAuthorizer : IAuthorize
    {
        private readonly Vkontakte _vkApi;
        private BotLongPollClient _client;


        public VkAuthorizer()
        {
            AccessToken accessToken = AccessToken.FromString(VkSettings.Key);
            _vkApi = new Vkontakte(VkSettings.AppId, VkSettings.AppSecret) { AccessToken = accessToken };
        }

        public void Auth()
        {
            GroupsLongPollServer settings = _vkApi.Groups.GetLongPollServer(VkSettings.GroupId).Result;
            
            _client = _vkApi.StartBotLongPollClient
            (
                settings.Server,
                settings.Key,
                Convert.ToInt32(settings.Ts)
            ).Result;
        }

        //TODO: null-checker to .ctor
        public Vkontakte GetApi()
        {
            return _vkApi ?? throw new BotValidException("Invalid VkApi");
        }

        public BotLongPollClient GetClient()
        {
            return _client ?? throw new BotValidException("Invalid VkLongPollClient");
        }
    }
}