using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ItmoSchedule.Migrations
{
    public partial class FirstInitial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BotSettings",
                columns: table => new
                {
                    Key = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BotSettings", x => x.Key);
                });

            migrationBuilder.CreateTable(
                name: "EventSettings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: true),
                    DateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventSettings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GroupSettings",
                columns: table => new
                {
                    GroupId = table.Column<string>(nullable: false),
                    GroupNumber = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupSettings", x => x.GroupId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BotSettings");

            migrationBuilder.DropTable(
                name: "EventSettings");

            migrationBuilder.DropTable(
                name: "GroupSettings");
        }
    }
}
