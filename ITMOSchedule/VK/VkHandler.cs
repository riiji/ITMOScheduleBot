using System;
using ITMOSchedule.Bot.Interfaces;
using VkNet;
using VkNet.Model;

namespace ITMOSchedule.Bot
{
    public class VkHandler : IHandler<LongPollHistoryResponse, string>
    {
        private readonly VkApi _vkApi;

        public VkHandler(VkApi vkApi)
        {
            throw new NotImplementedException();
        }

        public string HandleData(LongPollHistoryResponse data)
        {
            throw new NotImplementedException();
        }
    }
}