using System;
using System.Linq;
using ItmoSchedule.BotFramework.Exceptions;
using ItmoSchedule.BotFramework.Interfaces;
using ITMOSchedule.Common;
using ItmoScheduleApiWrapper;
using ItmoScheduleApiWrapper.Helpers;
using Telegram.Bot.Types.InlineQueryResults.Abstractions;

namespace ItmoSchedule.BotFramework.Commands.List
{
    public class GetGroupScheduleCommand : IBotCommand
    {
        private readonly IBotApiProvider _botProvider;
        private readonly ItmoApiProvider _itmoProvider;

        public GetGroupScheduleCommand(IBotApiProvider botProvider, ItmoApiProvider itmoProvider)
        {
            _botProvider = botProvider ?? throw new BotValidException("Bot provider not founded");
            _itmoProvider = itmoProvider ?? throw new BotValidException("Itmo provider not founded");
        }

        public string CommandName { get; } = "GetGroupSchedule";
        public string Description { get; } = "Get a group schedule by group number";

        public bool CanExecute(CommandArgumentContainer args)
        {
            return true;    
        }

        public CommandExecuteResult Execute(CommandArgumentContainer args)
        {
            var groupNumber = args.Arguments.FirstOrDefault();

            var lessonList = _itmoProvider
                .ScheduleApi
                .GetGroupSchedule(groupNumber)
                .Result
                .Schedule
                .GetTodaySchedule(DateConvertorService.FirstWeekEven);

            string result = string.Join(Environment.NewLine, 
                lessonList.Select(lesson => lesson.StartTime + " " + lesson.SubjectTitle));

            if (result == string.Empty)
                result = "There is nothing here";

            _botProvider.WriteMessage(args.GroupId, result);

            return new CommandExecuteResult(true);
        }
    }
}