using System;
using ItmoSchedule.Database;

namespace ItmoSchedule.VK
{
    public class VkSettings
    {
        public VkSettings()
        {
            using var db = new DatabaseContext();
            var settings = db.BotSettings;

            Key = settings.Find("VkKey").Value;
            AppId = Convert.ToInt32(settings.Find("VkAppId").Value);
            AppSecret = settings.Find("VkAppSecret").Value;
            GroupId = Convert.ToInt32(settings.Find("VkGroupId").Value);
        }

        public string Key { get; }

        public int AppId { get; }

        public string AppSecret { get; }

        public int GroupId { get; }
    }
}