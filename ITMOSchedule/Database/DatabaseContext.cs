using Microsoft.EntityFrameworkCore;
using ItmoSchedule.Database.Models;

namespace ItmoSchedule.Database
{
    public class DatabaseContext : DbContext
    {
        public DbSet<BotSettings> BotSettings { get; set; }
        public DbSet<EventSettings> EventSettings { get; set; }

        public DbSet<GroupSettings> GroupSettings { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Server=(localdb)\mssqllocaldb;Database=ITMOScheduleBot;Integrated Security=True");
        }
    }
}