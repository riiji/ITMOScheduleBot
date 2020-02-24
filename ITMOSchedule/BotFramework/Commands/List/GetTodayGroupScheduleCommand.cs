using System;
using System.Linq;
using ItmoSchedule.BotFramework.Exceptions;
using ItmoSchedule.BotFramework.Interfaces;
using ITMOSchedule.Common;
using ITMOSchedule.Extensions;
using ItmoScheduleApiWrapper;
using ItmoScheduleApiWrapper.Helpers;

namespace ItmoSchedule.BotFramework.Commands.List
{
    public class GetTodayGroupScheduleCommand : IBotCommand
    {
        private readonly IBotApiProvider _botProvider;
        private readonly ItmoApiProvider _itmoProvider;

        public GetTodayGroupScheduleCommand(IBotApiProvider botProvider, ItmoApiProvider itmoProvider)
        {
            _botProvider = botProvider ?? throw new BotValidException("Bot provider not founded");
            _itmoProvider = itmoProvider ?? throw new BotValidException("Itmo provider not founded");
        }

        public string CommandName { get; } = "GetTodayGroupSchedule";
        public string Description { get; } = "Get a group schedule by group number";
        public string[] Args { get; } = { "GroupNumber" };

        public bool CanExecute(CommandArgumentContainer args)
        {
            if (args.Arguments.Count != Args.Length)
            {
                _botProvider.WriteMessage(args.GroupId, "invalid arguments count");
                return false;
            }

            string groupNumber = args.Arguments.FirstOrDefault();

            if (groupNumber != null && groupNumber.Length != Utilities.GroupNameLength)
            {
                _botProvider.WriteMessage(args.GroupId, "invalid group number");
                return false;
            }
            
            return true;
        }

        public CommandExecuteResult Execute(CommandArgumentContainer args)
        {
            var groupNumber = args.Arguments.FirstOrDefault();

            var lessonListTask = _itmoProvider
                .ScheduleApi
                .GetGroupSchedule(groupNumber);
            lessonListTask.WaitSafe();

            if (lessonListTask.IsFaulted)
            {
                _botProvider.WriteMessage(args.GroupId, lessonListTask.Exception.Message);
                return new CommandExecuteResult(false);
            }

            var lessonList = lessonListTask.Result.Schedule.GetTodaySchedule(DateConvertorService.FirstWeekEven);

            var result = string.Join(Environment.NewLine,
                lessonList.Select(lesson => lesson.StartTime + " " + lesson.SubjectTitle));

            if (result == string.Empty)
                result = "There is nothing here";

            _botProvider.WriteMessage(args.GroupId, result);

            return new CommandExecuteResult(true);
        }
    }
}