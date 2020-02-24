using System;

namespace ItmoSchedule.Database.Models
{
    public class EventSettings
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public DateTime DateTime { get; set; }
    }
}