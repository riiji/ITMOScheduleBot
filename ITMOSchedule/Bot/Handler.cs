using System;
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
            throw new NotImplementedException();
        }

        public string HandleData(LongPollHistoryResponse data)
        {
            throw new NotImplementedException();
        }
    }
}