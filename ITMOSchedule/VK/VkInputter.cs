using System;
using System.Threading.Tasks;
using ITMOSchedule.Bot.Interfaces;
using ItmoScheduleApiWrapper;
using ItmoScheduleApiWrapper.Models;
using VkNet;
using VkNet.Model;
using VkNet.Model.RequestParams;

namespace ITMOSchedule.Bot
{
    public class VkInputter : IInput<LongPollHistoryResponse>
    {
        private readonly VkApi _api;

        private readonly LongPollServerResponse _response;

        public VkInputter(VkApi api)
        {
            throw new NotImplementedException();
        }

        public LongPollHistoryResponse GetData()
        {
            throw new NotImplementedException();
        }
    }
}