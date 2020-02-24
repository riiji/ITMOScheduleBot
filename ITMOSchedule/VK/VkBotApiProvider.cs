﻿using System;
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
        
        public void WriteMessage(int groupId, string message)
        {
            var result = _vkApi.Messages.Send(
                randomId: Utilities.GetRandom(),
                peerId: groupId,
                message: message);
            
            result.WaitSafe();

            if (result.IsFaulted)
            {
                if (result.Exception.InnerException is ApiException)
                    Auth();

                Utilities.Log(Utilities.LogLevel.Error, $"WriteMessage exception {result.Exception}");
            }
        }

        public void Dispose()
        {
            _client.OnMessageNew -= Client_OnMessageNew;
            _vkApi?.Dispose();
        }

        public void Auth()
        {
            AccessToken accessToken = AccessToken.FromString(_settings.Key);
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
                Utilities.Log(Utilities.LogLevel.Error, clientTask.Exception.Message);
                return;
            }

            _client = clientTask.Result;

            _client.OnMessageNew += Client_OnMessageNew;

            Utilities.Log(Utilities.LogLevel.Info, "Auth successfully");
        }

 
    }
}