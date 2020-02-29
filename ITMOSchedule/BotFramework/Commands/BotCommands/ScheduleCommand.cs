using System;
using System.Linq;
using ItmoSchedule.BotFramework.Interfaces;
using ITMOSchedule.Common;
using ItmoSchedule.Database;
using ITMOSchedule.Extensions;
using ItmoSchedule.Tools;
using ITMOSchedule.VK;
using ItmoScheduleApiWrapper;
using ItmoScheduleApiWrapper.Helpers;

namespace ItmoSchedule.BotFramework.Commands.BotCommands
{
    public class ScheduleCommand : IBotCommand
    {
        private readonly IBotApiProvider _botProvider;
        private readonly ItmoApiProvider _itmoProvider;

        public ScheduleCommand(IBotApiProvider botProvider, ItmoApiProvider itmoProvider)
        {
            _botProvider = botProvider;
            _itmoProvider = itmoProvider;
        }

        public string CommandName { get; } = "Schedule";
        public string Description { get; } = "Get a group command from bot settings";
        public string[] Args { get; } = { "GroupNumber:optional", "DateTime:optional" };

        public bool CanExecute(CommandArgumentContainer args)
        {
            return true;
        }

        public CommandExecuteResult Execute(CommandArgumentContainer args)
        {
            using var dbContext = new DatabaseContext();
            string groupName = string.Empty;
            var dateTime = DateTime.Today;

            switch (args.Arguments.Count)
            {
                case 0:
                    groupName = dbContext.GroupSettings.Find(args.Sender.GroupId.ToString()).GroupNumber;
                    break;
                case 1:
                    var groupNameOrDateTime = args.Arguments.FirstOrDefault();
                    var result = DateTime.TryParse(groupNameOrDateTime, out DateTime time);
                    if (result == false)
                        groupName = groupNameOrDateTime;
                    else
                    {
                        dateTime = time;
                        groupName = dbContext.GroupSettings.Find(args.Sender.GroupId.ToString()).GroupNumber;
                    }
                    break;
                case 2:
                    groupName = args.Arguments.FirstOrDefault();
                    var dateTimeResult = DateTime.TryParse(args.Arguments.Last(), out dateTime);
                    if (dateTimeResult == false) return new CommandExecuteResult(false, "invalid date");
                    Logger.Message(dateTime.ToShortDateString());
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

                var result = "Schedule on " +
                             scheduleDateTime.ToShortDateString() +
                             Environment.NewLine +
                             Environment.NewLine;

                if (schedule.Count == 0)
                    result += "Chilling time 👌🏻👌🏻👌🏻";
                else
                    result += string.Join(Environment.NewLine,
                    schedule.Select(lesson => "📌" + lesson.StartTime + "->" + lesson.SubjectTitle + $"({lesson.Status})" + " " + Environment.NewLine + lesson.Place + Environment.NewLine));

                return new CommandExecuteResult(true, result);
            }
        }
    }
}