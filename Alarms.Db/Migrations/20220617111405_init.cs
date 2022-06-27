using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Alarms.Db.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EventLogs",
                columns: table => new
                {
                    EventLogId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EventDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    State = table.Column<bool>(type: "bit", nullable: false),
                    TagDataId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventLogs", x => x.EventLogId);
                });

            migrationBuilder.CreateTable(
                name: "TagDatas",
                columns: table => new
                {
                    TagDataId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TagName = table.Column<string>(type: "varchar(50)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagDatas", x => x.TagDataId);
                    table.ForeignKey(
                        name: "FK_TagDatas_EventLogs_TagDataId",
                        column: x => x.TagDataId,
                        principalTable: "EventLogs",
                        principalColumn: "EventLogId",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TagDatas");

            migrationBuilder.DropTable(
                name: "EventLogs");
        }
    }
}
