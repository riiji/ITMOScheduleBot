using System.ComponentModel.DataAnnotations;

namespace ItmoSchedule.Database.Models
{
    public class GroupSettings
    {
        public GroupSettings(string groupId, string groupNumber)
        {
            GroupId = groupId;
            GroupNumber = groupNumber;
        }

        [Key]
        public string GroupId { get; set; }
        public string GroupNumber { get; set; }
    }
}