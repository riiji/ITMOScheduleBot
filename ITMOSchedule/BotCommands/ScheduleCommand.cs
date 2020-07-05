using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ItmoSchedule.Database;
using ItmoScheduleApiWrapper;
using ItmoScheduleApiWrapper.Helpers;
using ItmoScheduleApiWrapper.Models;
using MessengerBotFramework.Abstractions;
using MessengerBotFramework.Common;
using MessengerBotFramework.Tools;
using MessengerBotFramework.Tools.Extensions;

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
            try
            {
                var parseResult = ParseArguments(args, out string groupName, out DateTime dateTime);

                if (!parseResult.IsSuccess)
                    return parseResult;

                Task<GroupScheduleModel> scheduleTask = _itmoProvider.ScheduleApi.GetGroupSchedule(groupName);
                scheduleTask.WaitSafe();

                if (scheduleTask.IsFaulted)
                    return new Result(false, $"{groupName} from {args.Sender.GroupId} is invalid");

                string result = FormatResult(scheduleTask.Result, dateTime);
                return new Result(true, result);
            }
            catch (Exception e)
            {
                return new Result(false, $"ScheduleCommand from {args.Sender.GroupId} was failed with exception {e.Message}").WithException(e);
            }
        }

        private Result ParseArguments(CommandArgumentContainer args, out string groupName, out DateTime date)
        {
            try
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
                        return new Result(false, $"dateTime {dateAsString} from {args.Sender.GroupId} is invalid");
                    }
                }

                if (args.Arguments.Count == 2)
                {
                    groupName = args.Arguments.FirstOrDefault();
                    bool dateTimeResult = DateTime.TryParse(args.Arguments.Last(), out date);
                    if (dateTimeResult == false)
                        return new Result(false,
                            $"dateTime {args.Arguments.Last()} from {args.Sender.GroupId} is invalid");

                    LoggerHolder.Log.Information(date.ToShortDateString());
                    return new Result(true);
                }

                groupName = null;
                date = DateTime.MinValue;
                return new Result(false, $"{args.Sender.GroupId}, invalid arguments");
            }
            catch (Exception e)
            {
                groupName = string.Empty;
                date = DateTime.MinValue;

                return new Result(false, $"Parse argument from {args.Sender.GroupId} was failed with exception {e.Message}").WithException(e);
            }
        }

        private static string FormatResult(GroupScheduleModel groupSchedule, DateTime scheduleDateTime)
        {

            List<ScheduleItemModel> schedule = groupSchedule.Schedule.GetDaySchedule(scheduleDateTime, DateConvertorService.FirstWeekEven);

            string result = $"🔑Schedule [{groupSchedule.Label}]on " +
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