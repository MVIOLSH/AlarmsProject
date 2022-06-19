using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Alarms.Db.Migrations
{
    public partial class FixinmgRelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TagDatas_EventLogs_TagDataId",
                table: "TagDatas");

            migrationBuilder.CreateIndex(
                name: "IX_EventLogs_TagDataId",
                table: "EventLogs",
                column: "TagDataId");

            migrationBuilder.AddForeignKey(
                name: "FK_EventLogs_TagDatas_TagDataId",
                table: "EventLogs",
                column: "TagDataId",
                principalTable: "TagDatas",
                principalColumn: "TagDataId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventLogs_TagDatas_TagDataId",
                table: "EventLogs");

            migrationBuilder.DropIndex(
                name: "IX_EventLogs_TagDataId",
                table: "EventLogs");

            migrationBuilder.AddForeignKey(
                name: "FK_TagDatas_EventLogs_TagDataId",
                table: "TagDatas",
                column: "TagDataId",
                principalTable: "EventLogs",
                principalColumn: "EventLogId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
