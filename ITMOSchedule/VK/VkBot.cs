using System;
using System.Linq;
using System.Threading.Tasks;
using ITMOSchedule.Bot;
using ITMOSchedule.Bot.Commands;
using ITMOSchedule.Bot.Exceptions;
using ITMOSchedule.Commands;
using ITMOSchedule.Common;
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

            client.OnMessageNew += ClientOnMessageNew;
        }

        private void ClientOnMessageNew(object sender, MessagesMessage e)
        {
            var taskResult = CommandController(e.Text);

            // TODO: Logger
            if (taskResult.IsFaulted)
                return;

        }

        private Task CommandController(string text)
        {
            var splitted = text.Split();
            var command = splitted.FirstOrDefault();
            
            // skip command
            var args = splitted.Skip(1).ToArray();

            CommandHandler handler = new CommandHandler(new CommandsList());

            if (!handler.IsCommandExisted(command))
                return Task.FromException(new BotValidException("Command not existed"));

            CommandArgumentContainer arguments = new CommandArgumentContainer(args);

            handler.ExecuteCommand(command, arguments);

            return Task.CompletedTask;
        }

        public void Logout()
        {
            throw new NotImplementedException();
        }
    }
}