using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DowntimeAlerter.Migrations
{
    public partial class User_Create_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserGuid = table.Column<Guid>(nullable: false),
                    Username = table.Column<string>(maxLength: 1000, nullable: true),
                    Email = table.Column<string>(maxLength: 1000, nullable: true),
                    Password = table.Column<string>(nullable: true),
                    Role = table.Column<int>(nullable: false),
                    LastLoginDateUtc = table.Column<DateTime>(nullable: false),
                    FailedLoginAttempts = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
