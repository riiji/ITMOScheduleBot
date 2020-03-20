using System.Linq;
using ItmoSchedule.Abstractions;
using ItmoSchedule.Common;
using ItmoSchedule.Database;
using ItmoSchedule.Database.Models;

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
            using var dbContext = new DatabaseContext();

            var oldSetting = dbContext.GroupSettings.Find(args.Sender.GroupId.ToString());

            if (oldSetting != null)
                dbContext.GroupSettings.Remove(oldSetting);
             
            dbContext.GroupSettings.Add(new GroupSettings(args.Sender.GroupId.ToString(), args.Arguments.FirstOrDefault()));
            dbContext.SaveChanges();

            return new Result(true, "ok");
        }
    }
}