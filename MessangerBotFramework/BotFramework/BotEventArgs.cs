using System;

namespace MessengerBotFramework.BotFramework
{
    public class BotEventArgs : EventArgs
    {
        public BotEventArgs(string text, int groupId, int userSenderId)
        {
            Text = text;
            GroupId = groupId;
            UserSenderId = userSenderId;
        }

        public string Text { get; }

        public int GroupId { get; }

        public int UserSenderId { get; }
    }
}