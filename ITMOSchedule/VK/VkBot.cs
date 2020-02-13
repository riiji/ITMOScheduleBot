using System;
using ITMOSchedule.Bot;
using VkLibrary.Core;
using VkLibrary.Core.Auth;
using VkLibrary.Core.Objects;

namespace ITMOSchedule.VK
{
    public class VkBot
    {
        private readonly Vkontakte _vkApi;

        public VkBot()
        {
            AccessToken accessToken = AccessToken.FromString(VkSettings.Key);

            _vkApi = new Vkontakte(VkSettings.AppId, VkSettings.AppSecret) { AccessToken = accessToken };
        }

        public async void Login()
        {
            var settings = await _vkApi.Groups.GetLongPollServer(VkSettings.GroupId);

            var client = await _vkApi.StartBotLongPollClient
            (
                settings.Server,
                settings.Key,
                Convert.ToInt32(settings.Ts)
            );

            client.OnMessageNew += ClientOnOnMessageNew;
        }

        private void ClientOnOnMessageNew(object sender, MessagesMessage e)
        {
            CommandSplitter(e.Text);
        }

        private void CommandSplitter(string text)
        {
            var splitted = text.Split();
        }

        public void Logout()
        {
            throw new NotImplementedException();
        }
    }
}