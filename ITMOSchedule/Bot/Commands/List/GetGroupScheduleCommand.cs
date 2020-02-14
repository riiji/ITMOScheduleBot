using System;
using System.Linq;
using ITMOSchedule.Bot.Interfaces;
using ITMOSchedule.Commands;
using ITMOSchedule.Common;
using ItmoScheduleApiWrapper;
using ItmoScheduleApiWrapper.Helpers;
using ItmoScheduleApiWrapper.Types;

namespace ITMOSchedule.Bot.Commands.List
{
    public class GetGroupScheduleCommand : IBotCommand
    {
        private readonly IBotApiProvider _botProvider;
        private readonly ItmoApiProvider _itmoProvider;

        public GetGroupScheduleCommand(IBotApiProvider botProvider, ItmoApiProvider itmoProvider)
        {
            _botProvider = botProvider;
            _itmoProvider = itmoProvider;
        }

        public string CommandName { get; } = "GetGroupSchedule";
        public string Description { get; } = "Get a group schedule by group number";

        //TODO: get rid of execution
        public bool CanExecute(CommandArgumentContainer args)
        {
            if (_botProvider == null || _itmoProvider == null)
                return false;

            if (args.Arguments.Count != 1)
                return false;

            var groupNumber = args.Arguments.FirstOrDefault();
            
            var task = _itmoProvider.ScheduleApi.GetGroupSchedule(groupNumber);
            task.WaitSafe();

            return task.IsCompletedSuccessfully;
        }

        //TODO: Lesha says "Gavno, peredelyvai"
        public CommandExecuteResult Execute(CommandArgumentContainer args)
        {
            if(!CanExecute(args))
                return new CommandExecuteResult(false);

            var groupNumber = args.Arguments.FirstOrDefault();

            var lessonList = _itmoProvider.ScheduleApi.GetGroupSchedule(groupNumber).Result.Schedule.GetTodaySchedule(DateConvertorService.FirstWeekEven);

            string result = string.Join(Environment.NewLine, lessonList.Select(lesson => lesson.StartTime + " " + lesson.SubjectTitle));
            //string result = lessonList.Aggregate("", (current, lesson) => current + (lesson.StartTime + " " + lesson.SubjectTitle + Environment.NewLine));

            if (result == string.Empty)
                result = "There is nothing here";

            _botProvider.WriteMessage(args.GroupId, result);  

            return new CommandExecuteResult(true);
        }
    }
}