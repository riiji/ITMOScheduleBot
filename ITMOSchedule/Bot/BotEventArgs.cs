using System;

namespace ITMOSchedule.Bot
{
    public class BotEventArgs : EventArgs
    {
        public BotEventArgs(string text, int groupId)
        {
            Text = text;
            GroupId = groupId;
        }

        public string Text { get; set; }

        public int GroupId { get; set; }
    }
}