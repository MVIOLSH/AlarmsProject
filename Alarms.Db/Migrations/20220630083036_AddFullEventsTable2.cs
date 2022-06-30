using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Alarms.Db.Migrations
{
    public partial class AddFullEventsTable2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Duration",
                table: "FullEvents");

            migrationBuilder.AddColumn<int>(
                name: "DurationSeconds",
                table: "FullEvents",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DurationSeconds",
                table: "FullEvents");

            migrationBuilder.AddColumn<decimal>(
                name: "Duration",
                table: "FullEvents",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
