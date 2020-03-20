using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ItmoSchedule.Abstractions;
using ItmoSchedule.Common;
using ItmoSchedule.Database;
using ItmoSchedule.Tools.Extensions;
using ItmoSchedule.Tools.Loggers;
using ItmoScheduleApiWrapper;
using ItmoScheduleApiWrapper.Helpers;
using ItmoScheduleApiWrapper.Models;

namespace ItmoSchedule.BotCommands
{
    public class ScheduleCommand : IBotCommand
    {
        private readonly ItmoApiProvider _itmoProvider;

        public ScheduleCommand()
        {
            _itmoProvider = new ItmoApiProvider();
        }

        public string CommandName { get; } = "Schedule";
        public string Description { get; } = "Get a group command from bot settings";
        public string[] Args { get; } = { "GroupNumber:optional", "DateTime:optional" };

        public bool CanExecute(CommandArgumentContainer args)
        {
            return true;
        }

        public Result Execute(CommandArgumentContainer args)
        {
            var parseResult = ParseArguments(args, out string groupName, out DateTime dateTime);

            if (!parseResult.IsSuccess)
                return parseResult;

            Task<GroupScheduleModel> scheduleTask = _itmoProvider.ScheduleApi.GetGroupSchedule(groupName);
            scheduleTask.WaitSafe();

            if (scheduleTask.IsFaulted)
                return new Result(false, "invalid group number");

            string result = FormatResult(scheduleTask.Result, dateTime);
            return new Result(true, result);
        }

        private Result ParseArguments(CommandArgumentContainer args, out string groupName, out DateTime date)
        {
            using var dbContext = new DatabaseContext();

            if (args.Arguments.Count == 0)
            {
                groupName = dbContext.GroupSettings.Find(args.Sender.GroupId.ToString()).GroupNumber;
                date = DateTime.Today;
                return new Result(true);
            }

            if (args.Arguments.Count == 1)
            {
                string dateAsString = args.Arguments.FirstOrDefault();
                bool result = DateTime.TryParse(dateAsString, out date);
                if (result)
                {
                    groupName = dbContext.GroupSettings.Find(args.Sender.GroupId.ToString()).GroupNumber;
                    return new Result(true);
                }
                else
                {
                    groupName = null;
                    return new Result(false, $"Can't parse to DateTime: {dateAsString}");
                }
            }

            if (args.Arguments.Count == 2)
            {
                groupName = args.Arguments.FirstOrDefault();
                bool dateTimeResult = DateTime.TryParse(args.Arguments.Last(), out date);
                if (dateTimeResult == false)
                    return new Result(false, $"Can't parse to DateTime: {args.Arguments.Last()}");

                Logger.Message(date.ToShortDateString());
                return new Result(true);
            }

            groupName = null;
            date = DateTime.MinValue;
            return new Result(false, "invalid arguments");
        }

        private static string FormatResult(GroupScheduleModel groupSchedule, DateTime scheduleDateTime)
        {
            List<ScheduleItemModel> schedule = groupSchedule.Schedule.GetDaySchedule(scheduleDateTime, DateConvertorService.FirstWeekEven);

            string result = "Schedule on " +
                            scheduleDateTime.ToShortDateString() +
                            Environment.NewLine +
                            Environment.NewLine;

            if (schedule.Count == 0)
                result += "Chilling time 👌🏻👌🏻👌🏻";
            else
                result += string.Join(Environment.NewLine,
                    schedule.Select(lesson =>
                        $"📌{lesson.StartTime}->{lesson.SubjectTitle}({lesson.Status}) {Environment.NewLine}{lesson.Place}{Environment.NewLine}"));

            return result;
        }
    }
}