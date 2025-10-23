using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EnterpriseApp.Repository.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Log",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LogDate = table.Column<DateTime>(nullable: false),
                    Application = table.Column<string>(maxLength: 50, nullable: false),
                    Level = table.Column<string>(maxLength: 50, nullable: false),
                    Message = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 250, nullable: true),
                    ServerName = table.Column<string>(maxLength: 64, nullable: true),
                    MachineName = table.Column<string>(maxLength: 64, nullable: true),
                    Port = table.Column<string>(maxLength: 256, nullable: true),
                    Url = table.Column<string>(maxLength: 500, nullable: true),
                    ServerAddress = table.Column<string>(maxLength: 100, nullable: true),
                    RemoteAddress = table.Column<string>(maxLength: 100, nullable: true),
                    Logger = table.Column<string>(maxLength: 250, nullable: true),
                    Exception = table.Column<string>(nullable: true),
                    StackTrace = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Log", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserName = table.Column<string>(maxLength: 128, nullable: false),
                    UserPassword = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Configuration",
                columns: table => new
                {
                    Group = table.Column<string>(maxLength: 256, nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: false),
                    Type = table.Column<string>(maxLength: 256, nullable: false),
                    Value = table.Column<string>(maxLength: 4000, nullable: true),
                    UserId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Configuration", x => new { x.Group, x.Name });
                    table.ForeignKey(
                        name: "FK_Configuration_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Configuration_UserId",
                table: "Configuration",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_User_UserName",
                table: "User",
                column: "UserName",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Configuration");

            migrationBuilder.DropTable(
                name: "Log");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
