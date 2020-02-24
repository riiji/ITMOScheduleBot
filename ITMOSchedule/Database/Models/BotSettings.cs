using System.ComponentModel.DataAnnotations;

namespace ItmoSchedule.Database.Models
{
    public class BotSettings
    {
        [Key]
        public string Key { get; set; }
        public string Value { get; set; }
    }
}