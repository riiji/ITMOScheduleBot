namespace MessengerBotFramework.MessangerApiProviders.VK
{
    public class VkSettings
    {
        public VkSettings(string key, int appId, string appSecret, int groupId)
        {
            Key = key;
            AppId = appId;
            AppSecret = appSecret;
            GroupId = groupId;
        }

        public string Key { get; }

        public int AppId { get; }

        public string AppSecret { get; }

        public int GroupId { get; }
    }
}