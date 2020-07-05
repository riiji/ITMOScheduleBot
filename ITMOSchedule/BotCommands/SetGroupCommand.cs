using System;
using System.Linq;
using ItmoSchedule.Database;
using ItmoSchedule.Database.Models;
using MessengerBotFramework.Abstractions;
using MessengerBotFramework.Common;

namespace ItmoSchedule.BotCommands
{
    public class SetGroupCommand : IBotCommand
    {
        public string CommandName { get; } = "SetGroup";
        public string Description { get; } = "Set a default group to get schedule without group number";
        public string[] Args { get; } = {"GroupNumber"};
        public bool CanExecute(CommandArgumentContainer args)
        {
            return true;
        }

        public Result Execute(CommandArgumentContainer args)
        {
            try
            {
                using var dbContext = new DatabaseContext();

                var oldSetting = dbContext.GroupSettings.Find(args.Sender.GroupId.ToString());

                if (oldSetting != null)
                    dbContext.GroupSettings.Remove(oldSetting);

                dbContext.GroupSettings.Add(new GroupSettings(args.Sender.GroupId.ToString(), args.Arguments.FirstOrDefault()));
                dbContext.SaveChanges();

                return new Result(true, $"SetGroup from {args.Sender.GroupId} with {args.Arguments.FirstOrDefault()} was successfully");
            }
            catch (Exception e)
            {
                return new Result(false, $"SetGroup from {args.Sender.GroupId} was with exception {e.Message}").WithException(e);
            }
        }
    }
}