using System;
using System.Collections.Generic;
using System.Linq;
using ItmoSchedule.BotFramework.Interfaces;
using ITMOSchedule.Common;
using ItmoSchedule.Database;
using ITMOSchedule.Extensions;
using ItmoScheduleApiWrapper;
using ItmoScheduleApiWrapper.Helpers;
using ItmoScheduleApiWrapper.Models;

namespace ItmoSchedule.BotFramework.Commands.BotCommands
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
            switch (args.Arguments.Count)
            {
                case 0: return InnerExecute(groupId: args.GroupId.ToString());
                case 1: return InnerExecute(args.Arguments.FirstOrDefault());
                case 2:
                    var dateTimeResult = DateTime.TryParse(args.Arguments.Last(), out DateTime time);
                    if (dateTimeResult == false) return new CommandExecuteResult(false);
                    return InnerExecute(args.Arguments.FirstOrDefault(), time);
                
                default: return new CommandExecuteResult(false);
            }

            CommandExecuteResult InnerExecute(string groupName = null, DateTime? scheduleDateTime = null, string groupId = null)
            {
                using var dbContext = new DatabaseContext();
                
                // if groupName is null, then trying to find groupNumber from Db
                string group = groupName ?? dbContext.GroupSettings.Find(groupId).GroupNumber;

                if (group == null)
                    return new CommandExecuteResult(false);

                var scheduleTask = _itmoProvider.ScheduleApi.GetGroupSchedule(group);
                scheduleTask.WaitSafe();

                if (scheduleTask.IsFaulted)
                    return new CommandExecuteResult(false);

                // if schedule date time is null, then get today, if not, take schedule via date
                var schedule = scheduleDateTime == null 
                    ? scheduleTask.Result.Schedule.GetTodaySchedule(DateConvertorService.FirstWeekEven) 
                    : scheduleTask.Result.Schedule.GetDaySchedule(scheduleDateTime.Value, DateConvertorService.FirstWeekEven);

                var result = string.Join(Environment.NewLine,
                    schedule.Select(lesson => lesson.StartTime + " " + lesson.SubjectTitle));

                if (result == string.Empty)
                    result = "There is nothing here";

                _botProvider.WriteMessage(args.GroupId, result);

                return new CommandExecuteResult(true);
            }
        }
    }
}