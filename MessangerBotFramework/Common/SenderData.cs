namespace MessengerBotFramework.Common
{
    public class SenderData
    {
        public SenderData(int groupId)
        {
            GroupId = groupId;
        }

        public int GroupId { get; }
    }
}