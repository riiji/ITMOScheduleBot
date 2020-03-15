using System.Threading.Tasks;
using ItmoSchedule.Abstractions;
using ItmoSchedule.BotFramework;
using ItmoSchedule.Common;
using ItmoSchedule.Tools.Extensions;
using VkApi.Wrapper.Methods;

namespace ItmoSchedule.VK
{
    public class VkWriteMessage : IWriteMessage
    {
        private readonly Messages _vkMessages;

        public VkWriteMessage(Messages vkMessages)
        {
            _vkMessages = vkMessages;
        }

        public TaskExecuteResult WriteMessage(SenderData sender, string message)
        {
            var sendMessageTask = _vkMessages.Send(
                randomId: Utilities.GetRandom(),
                peerId: sender.GroupId,
                message: message);

            sendMessageTask.WaitSafe();

            if (sendMessageTask.IsFaulted)
                return new TaskExecuteResult(false, "Vk write message failed").WithException(sendMessageTask.Exception);
            return new TaskExecuteResult(true, "vk write message ok");
        }
    }
}