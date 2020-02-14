using System;
using System.Threading.Tasks;
using ITMOSchedule.Bot;
using ITMOSchedule.Bot.Interfaces;
using VkApi.Wrapper;
using VkApi.Wrapper.Types.Messages;

namespace ITMOSchedule.VK
{
    public class VkBotApiProvider : IBotApiProvider
    {
        private readonly Vkontakte _vkApi;

        public event IBotApiProvider.MessageDelegate OnMessage;

        public VkBotApiProvider(VkAuthorizer vkAuth)
        {
            var vkAuthorizer = vkAuth;
            vkAuthorizer.Auth();

            _vkApi = vkAuthorizer.GetApi();

            var client = vkAuthorizer.GetClient();

            client.OnMessageNew += Client_OnMessageNew;
        }

        private void Client_OnMessageNew(object sender, MessagesMessage e)
        {
            OnMessage?.Invoke(sender, new BotEventArgs(e.Text, e.PeerId));
        }
        
        public Task WriteMessage(int groupId, string message)
        {
            var result = _vkApi.Messages.Send(null, Utilities.GetRandom(), groupId, null, null, null, message);
            
            result.WaitSafe();

            Console.WriteLine(result.Exception);

            return Task.CompletedTask;
        }
    }
}