using System.Threading.Tasks;
using ITMOSchedule.Bot.Interfaces;
using ItmoScheduleApiWrapper;
using ItmoScheduleApiWrapper.Models;
using VkNet;
using VkNet.Model;
using VkNet.Model.RequestParams;

namespace ITMOSchedule.Bot
{
    public class Inputter : IInput<LongPollHistoryResponse>
    {
        private readonly VkApi _api;

        private readonly LongPollServerResponse _response;

        public Inputter(VkApi api)
        {
            _api = api;

            _response = _api.Groups.GetLongPollServer(Utilities.BotConfig.GroupId);
        }

        public LongPollHistoryResponse GetData()
        {
            var response = _api.Messages.GetLongPollHistory(new MessagesGetLongPollHistoryParams
            {
                Ts = ulong.Parse(_response.Ts),
                Pts = _response.Pts,
            });

            _response.Pts = response.NewPts;

            return response;
        }
    }
}