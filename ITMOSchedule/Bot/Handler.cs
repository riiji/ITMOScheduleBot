using ITMOSchedule.Bot.Interfaces;
using VkNet;
using VkNet.Model;

namespace ITMOSchedule.Bot
{
    public class Handler : IHandler<LongPollHistoryResponse, string>
    {
        private readonly VkApi _vkApi;

        public Handler(VkApi vkApi)
        {
            _vkApi = vkApi;
        }

        public string HandleData(LongPollHistoryResponse data)
        {
            return null;
        }
    }
}