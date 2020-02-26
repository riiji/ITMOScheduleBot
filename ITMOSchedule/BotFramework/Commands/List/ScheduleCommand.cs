using System;
using System.Linq;
using ItmoSchedule.BotFramework.Interfaces;
using ITMOSchedule.Common;
using ItmoSchedule.Database;
using ITMOSchedule.Extensions;
using ItmoScheduleApiWrapper;
using ItmoScheduleApiWrapper.Helpers;

namespace ItmoSchedule.BotFramework.Commands.List
{
    public class ScheduleCommand : IBotCommand
    {
        private IBotApiProvider _botProvider;
        private ItmoApiProvider _itmoProvider;

        public ScheduleCommand(IBotApiProvider botProvider, ItmoApiProvider itmoProvider)
        {
            _itmoProvider = itmoProvider;
            _botProvider = botProvider;
        }

        public string CommandName { get; } = "Schedule";
        public string Description { get; } = "Get a group command from bot settings";
        public string[] Args { get; } = { };

        public bool CanExecute(CommandArgumentContainer args)
        {
            return true;
        }

        public CommandExecuteResult Execute(CommandArgumentContainer args)
        {
            using var dbContext = new DatabaseContext();

            var group = dbContext.GroupSettings.Find(args.GroupId);
            
            if(group==null)
                return new CommandExecuteResult(false);

            var scheduleTask = _itmoProvider.ScheduleApi.GetGroupSchedule(group.GroupNumber);
            scheduleTask.WaitSafe();

            if(scheduleTask.IsFaulted)
                return new CommandExecuteResult(false);

            var schedule = scheduleTask.Result.Schedule.GetTodaySchedule(DateConvertorService.FirstWeekEven);

            var result = string.Join(Environment.NewLine,
                schedule.Select(lesson => lesson.StartTime + " " + lesson.SubjectTitle));

            if (result == string.Empty)
                result = "There is nothing here";

            _botProvider.WriteMessage(args.GroupId, result);

            return new CommandExecuteResult(true);
        }
    }
}