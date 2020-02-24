using System;

namespace ItmoSchedule.BotFramework
{
    public class BotEventArgs : EventArgs
    {
        public BotEventArgs(string text, int groupId, int userSenderId)
        {
            Text = text;
            GroupId = groupId;
            UserSenderId = userSenderId;
        }

        public string Text { get; set; }

        public int GroupId { get; set; }

        public int UserSenderId { get; set; }
    }
}