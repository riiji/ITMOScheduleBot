using System;
using System.Linq;
using System.Threading.Tasks;
using ItmoSchedule.Abstractions;
using ItmoSchedule.BotFramework;
using ItmoSchedule.Common;
using ItmoSchedule.Tools.Extensions;
using ItmoSchedule.Tools.Loggers;
using ItmoSchedule.VK;
using Newtonsoft.Json.Linq;
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
            _client.LongPollFailureReceived -= Client_OnFail;
        }
        public Result WriteMessage(SenderData sender, string message)
        {
            var sendMessageTask = _vkApi.Messages.Send(
                randomId: Utilities.GetRandom(),
                peerId: sender.GroupId,
                message: message);

            sendMessageTask.WaitSafe();

            if (sendMessageTask.IsFaulted)
                return new Result(false, $"Vk write message failed from {sender.GroupId} with message {message}").WithException(sendMessageTask.Exception);
            return new Result(true, $"Vk write {message} to {sender.GroupId} ok");
        }

        public Result Initialize()
        {
            var accessToken = AccessToken.FromString(_settings.Key);
            _vkApi = new Vkontakte(_settings.AppId, _settings.AppSecret) { AccessToken = accessToken };
            Task<GroupsLongPollServer> getSettingsTask = _vkApi.Groups.GetLongPollServer(_settings.GroupId);

            getSettingsTask.WaitSafe();

            if (getSettingsTask.IsFaulted)
                return new Result(false, "Get long poll server failed").WithException(getSettingsTask.Exception);

            var settings = getSettingsTask.Result;

            Task<BotLongPollClient> clientTask = _vkApi.StartBotLongPollClient
            (
                settings.Server,
                settings.Key,
                Convert.ToInt32(settings.Ts)
            );

            clientTask.WaitSafe();

            if (clientTask.IsFaulted)
            {
                return new Result(false, "Auth is failed").WithException(clientTask.Exception);
            }

            _client = clientTask.Result;
            _client.OnMessageNew += Client_OnMessageNew;
            _client.LongPollFailureReceived += Client_OnFail;
            _client.ResponseReceived += Client_OnResponse;

            return new Result(true, "Auth successfully");
        }

        private void Client_OnResponse(object? sender, JArray e)
        {
            Logger.Info($"Response: {string.Join(' ',e.ToArray().Select(x=>x.ToString()))}");
        }

        private void Client_OnFail(object? sender, int e)
        {
            Logger.Error($"VkBotApiProvider_Client_OnFail with {e}");

            var settings = _vkApi.Groups.GetLongPollServer(_settings.GroupId).Result;
            var client = _vkApi.StartBotLongPollClient
            (
                settings.Server,
                settings.Key,
                Convert.ToInt32(settings.Ts)
            ).Result;

            _client = client;
            _client.OnMessageNew += Client_OnMessageNew;
            _client.LongPollFailureReceived += Client_OnFail;
            _client.ResponseReceived += Client_OnResponse;
        }


    }
}