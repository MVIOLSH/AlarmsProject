using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Alarms.Db.Migrations
{
    public partial class AddFullEventsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FullEvents",
                columns: table => new
                {
                    FullEventsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TagDataId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EventStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EventEnd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Duration = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FullEvents", x => x.FullEventsId);
                    table.ForeignKey(
                        name: "FK_FullEvents_TagDatas_TagDataId",
                        column: x => x.TagDataId,
                        principalTable: "TagDatas",
                        principalColumn: "TagDataId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FullEvents_TagDataId",
                table: "FullEvents",
                column: "TagDataId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FullEvents");
        }
    }
}
