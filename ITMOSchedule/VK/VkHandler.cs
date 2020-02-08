using System;
using ITMOSchedule.Bot.Interfaces;
using VkNet;
using VkNet.Model;

namespace ITMOSchedule.Vk
{
    public class VkHandler 
    { 
        private readonly VkApi _vkApi;

        public VkHandler(VkApi vkApi)
        {
            _vkApi = vkApi;
        }

        public string HandleData(LongPollHistoryResponse data)
        {
            throw new NotImplementedException();
        }
    }
}