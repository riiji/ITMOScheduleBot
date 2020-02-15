using System;
using System.Threading.Tasks;
using ITMOSchedule.Bot;
using ITMOSchedule.Bot.Interfaces;
using VkApi.Wrapper;
using VkApi.Wrapper.Types.Messages;

namespace ITMOSchedule.VK
{
    //TODO: dispose?
    public class VkBotApiProvider : IBotApiProvider
    {
        private readonly Vkontakte _vkApi;

        public event EventHandler<BotEventArgs> OnMessage;

        public VkBotApiProvider(VkAuthorizer vkAuth)
        {
            var vkAuthorizer = vkAuth;
            vkAuthorizer.Auth();

            //TODO: huinya, peredelyvai
            _vkApi = vkAuthorizer.GetApi();

            var client = vkAuthorizer.GetClient();

            //TODO: dispose
            client.OnMessageNew += Client_OnMessageNew;
        }

        private void Client_OnMessageNew(object sender, MessagesMessage e)
        {
            OnMessage?.Invoke(sender, new BotEventArgs(e.Text, e.PeerId));
        }
        
        //TODO: remove Task
        public Task WriteMessage(int groupId, string message)
        {
            var result = _vkApi.Messages.Send(
                randomId: Utilities.GetRandom(),
                peerId: groupId,
                message: message);
            
            //TODO: return failed state
            result.WaitSafe();

            //TODO: write to logger only if exception exists
            Console.WriteLine(result.Exception);

            return Task.CompletedTask;
        }
    }
}