using System.Linq;
using ItmoSchedule.BotFramework.Interfaces;
using ITMOSchedule.Common;
using ItmoSchedule.Database;
using ItmoSchedule.Database.Models;
using ITMOSchedule.VK;

namespace ItmoSchedule.BotFramework.Commands.BotCommands
{
    public class SetGroupCommand : IBotCommand
    {
        private readonly IBotApiProvider _botProvider;
        public SetGroupCommand(IBotApiProvider botProvider)
        {
            _botProvider = botProvider;
        }

        public string CommandName { get; } = "SetGroup";
        public string Description { get; } = "Set a default group to get schedule without group number";
        public string[] Args { get; } = {"GroupNumber"};
        public bool CanExecute(CommandArgumentContainer args)
        {
            return true;
        }

        public CommandExecuteResult Execute(CommandArgumentContainer args)
        {
            using var dbContext = new DatabaseContext();

            var oldSetting = dbContext.GroupSettings.Find(args.Sender.GroupId.ToString());

            if (oldSetting != null)
                dbContext.GroupSettings.Remove(oldSetting);
             
            dbContext.GroupSettings.Add(new GroupSettings(args.Sender.GroupId.ToString(), args.Arguments.FirstOrDefault()));
            dbContext.SaveChanges();

            return new CommandExecuteResult(true, "ok");
        }
    }
}