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
        private readonly ItmoApiProvider _itmoProvider;

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
            string groupName;
            DateTime dateTime = DateTime.Today;

            switch (args.Arguments.Count)
            {
                case 0:
                    groupName = dbContext.GroupSettings.Find(args.Sender.GroupId.ToString()).GroupNumber;
                    break;
                case 1:
                    groupName = args.Arguments.FirstOrDefault();
                    break;

                case 2:
                    groupName = args.Arguments.FirstOrDefault();
                    var dateTimeResult = DateTime.TryParse(args.Arguments.Last(), out dateTime);
                    if (dateTimeResult == false) return new CommandExecuteResult(false, "invalid date");
                    break;

                default: return new CommandExecuteResult(false, "invalid arguments");
            }

            return InnerExecute(groupName, dateTime);

            CommandExecuteResult InnerExecute(string groupName, DateTime scheduleDateTime)
            {
                var scheduleTask = _itmoProvider.ScheduleApi.GetGroupSchedule(groupName);
                scheduleTask.WaitSafe();

                if (scheduleTask.IsFaulted)
                    return new CommandExecuteResult(false, "invalid group number");

                var schedule = scheduleTask.Result.Schedule.GetDaySchedule(scheduleDateTime, DateConvertorService.FirstWeekEven);

                var result = string.Join(Environment.NewLine,
                    schedule.Select(lesson => lesson.StartTime + " " + lesson.SubjectTitle));

                if (result == string.Empty)
                    result = "There is nothing here";

                return new CommandExecuteResult(true, result);
            }
        }
    }
}