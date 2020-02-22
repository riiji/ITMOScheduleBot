using System;
using ItmoSchedule.BotFramework;
using ItmoSchedule.BotFramework.Interfaces;
using ITMOSchedule.Extensions;
using ItmoSchedule.VK;
using VkApi.Wrapper;
using VkApi.Wrapper.Auth;
using VkApi.Wrapper.LongPolling.Bot;
using VkApi.Wrapper.Types.Groups;
using VkApi.Wrapper.Types.Messages;

namespace ITMOSchedule.VK
{
    public class VkBotApiProvider : IBotApiProvider, IDisposable
    {
        private Vkontakte _vkApi;
        private BotLongPollClient _client;
        public event EventHandler<BotEventArgs> OnMessage;

        private void Client_OnMessageNew(object sender, MessagesMessage e)
        {
            OnMessage?.Invoke(sender, new BotEventArgs(e.Text, e.PeerId));
        }
        
        public void WriteMessage(int groupId, string message)
        {
            var result = _vkApi.Messages.Send(
                randomId: Utilities.GetRandom(),
                peerId: groupId,
                message: message);
            
            result.WaitSafe();

            if (result.IsFaulted)
                Utilities.Log(Utilities.LogLevel.Error, $"Write message exception {result.Exception}");
        }

        public void Dispose()
        {
            _client.OnMessageNew -= Client_OnMessageNew;
            _vkApi?.Dispose();
        }

        public void Auth()
        {
            AccessToken accessToken = AccessToken.FromString(VkSettings.Key);
            _vkApi = new Vkontakte(VkSettings.AppId, VkSettings.AppSecret) { AccessToken = accessToken };

            GroupsLongPollServer settings = _vkApi.Groups.GetLongPollServer(VkSettings.GroupId).Result;

            _client = _vkApi.StartBotLongPollClient
            (
                settings.Server,
                settings.Key,
                Convert.ToInt32(settings.Ts)
            ).Result;

            _client.OnMessageNew += Client_OnMessageNew;

            Utilities.Log(Utilities.LogLevel.Info, "Auth successfully");
        }
    }
}