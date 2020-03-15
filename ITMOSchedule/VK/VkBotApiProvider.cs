using System;
using ItmoSchedule.Abstractions;
using ItmoSchedule.BotFramework;
using ItmoSchedule.Common;
using ItmoSchedule.Tools.Extensions;
using ItmoSchedule.Tools.Loggers;
using ItmoSchedule.VK;
using VkApi.Wrapper;
using VkApi.Wrapper.Auth;
using VkApi.Wrapper.LongPolling.Bot;
using VkApi.Wrapper.Types.Groups;
using VkApi.Wrapper.Types.Messages;

namespace ItmoSchedule.VK
{
    public class VkBotApiProvider : IBotApiProvider, IDisposable
    {
        private Vkontakte _vkApi;
        private BotLongPollClient _client;
        private readonly VkSettings _settings;

        public VkBotApiProvider(VkSettings settings)
        {
            _settings = settings;
        }

        public event EventHandler<BotEventArgs> OnMessage;

        private void Client_OnMessageNew(object sender, MessagesMessage e)
        {
            OnMessage?.Invoke(sender, new BotEventArgs(e.Text, e.PeerId,e.FromId));
        }

        public void Dispose()
        {
            _client.OnMessageNew -= Client_OnMessageNew;
            _vkApi?.Dispose();
        }

        public TaskExecuteResult Initialize()
        {
            var accessToken = AccessToken.FromString(_settings.Key);
            _vkApi = new Vkontakte(_settings.AppId, _settings.AppSecret) { AccessToken = accessToken };
            GroupsLongPollServer settings = _vkApi.Groups.GetLongPollServer(_settings.GroupId).Result;

            var clientTask = _vkApi.StartBotLongPollClient
            (
                settings.Server,
                settings.Key,
                Convert.ToInt32(settings.Ts)
            );

            clientTask.WaitSafe();

            if (clientTask.IsFaulted)
            {
                Logger.Error($"Failed to process Auth, handle exception:{clientTask.Exception.Message}");

                return new TaskExecuteResult(false, "Auth is failed").WithException(clientTask.Exception);
            }

            _client = clientTask.Result;

            _client.OnMessageNew += Client_OnMessageNew;

            return new TaskExecuteResult(true, "Auth successfully");
        }

        public Vkontakte GetApi()
        {
            return _vkApi;
        }
    }
}