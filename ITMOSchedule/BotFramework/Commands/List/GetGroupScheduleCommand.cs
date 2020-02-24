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
        public string Description { get; } = "Get group schedule in a date day";
        public string[] Args { get; } = {"GroupNumber","Date"};


        public bool CanExecute(CommandArgumentContainer args)
        {
            if (args.Arguments.Count != Args.Length)
                return false;

            var group = args.Arguments.FirstOrDefault();

            if (group != null && group.Length != Utilities.GroupNameLength)
                return false;

            var dateTimeResult = DateTime.TryParse(args.Arguments.Last(), out DateTime time);
            
            if (dateTimeResult == false)
                return false;

            return true;
        }

        public CommandExecuteResult Execute(CommandArgumentContainer args)
        {
            var groupNumber = args.Arguments.FirstOrDefault();
            var dateTime = DateTime.Parse(args.Arguments.Last());

            var lessonListTask = _itmoProvider.ScheduleApi.GetGroupSchedule(groupNumber);
            lessonListTask.WaitSafe();

            if (lessonListTask.IsFaulted)
            {
                _botProvider.WriteMessage(args.GroupId, lessonListTask.Exception.Message);
                return new CommandExecuteResult(false);
            }

            var lessonList =
                lessonListTask.Result.Schedule.GetDaySchedule(dateTime, DateConvertorService.FirstWeekEven);

            var result = string.Join(Environment.NewLine,
                lessonList.Select(lesson => lesson.StartTime + " " + lesson.SubjectTitle));

            if (result == string.Empty)
                result = "There is nothing here";

            _botProvider.WriteMessage(args.GroupId, result);

            return new CommandExecuteResult(true);
        }
    }
}