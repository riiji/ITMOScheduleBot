using System.Linq;
using ItmoSchedule.BotFramework.Interfaces;
using ITMOSchedule.Common;
using ItmoSchedule.Database;
using ItmoSchedule.Database.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

namespace ItmoSchedule.BotFramework.Commands.List
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
            if (args.Arguments.Count != Args.Length)
            {
                _botProvider.WriteMessage(args.GroupId, "invalid arguments count");
                return false;
            }

            string groupNumber = args.Arguments.FirstOrDefault();

            if (groupNumber != null && groupNumber.Length != Utilities.GroupNameLength)
            {
                _botProvider.WriteMessage(args.GroupId, "invalid group number");
                return false;
            }

            return true;
        }

        public CommandExecuteResult Execute(CommandArgumentContainer args)
        {
            using var dbContext = new DatabaseContext();

            var oldSetting = dbContext.GroupSettings.Find(args.GroupId);
            dbContext.GroupSettings.Remove(oldSetting);
            dbContext.GroupSettings.Add(new GroupSettings(args.GroupId.ToString(), args.Arguments.FirstOrDefault()));
            dbContext.SaveChanges();

            return new CommandExecuteResult(true);
        }
    }
}