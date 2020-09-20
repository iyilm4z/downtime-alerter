using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DowntimeAlerter.Migrations
{
    public partial class TargetApp_Update_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LastCheckDate",
                table: "TargetApplication",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastCheckDate",
                table: "TargetApplication");
        }
    }
}
