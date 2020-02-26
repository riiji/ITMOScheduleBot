using Microsoft.EntityFrameworkCore.Migrations;

namespace ItmoSchedule.Migrations
{
    public partial class group_settings_added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "GroupSettings");
        }
    }
}
