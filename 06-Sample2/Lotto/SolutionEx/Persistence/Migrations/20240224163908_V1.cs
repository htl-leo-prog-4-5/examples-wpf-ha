using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class V1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Game",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateFrom = table.Column<DateOnly>(type: "date", nullable: false),
                    DateTo = table.Column<DateOnly>(type: "date", nullable: false),
                    ExpectedDrawDate = table.Column<DateOnly>(type: "date", nullable: false),
                    DrawDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    No1 = table.Column<byte>(type: "tinyint", nullable: true),
                    No2 = table.Column<byte>(type: "tinyint", nullable: true),
                    No3 = table.Column<byte>(type: "tinyint", nullable: true),
                    No4 = table.Column<byte>(type: "tinyint", nullable: true),
                    No5 = table.Column<byte>(type: "tinyint", nullable: true),
                    No6 = table.Column<byte>(type: "tinyint", nullable: true),
                    NoX = table.Column<byte>(type: "tinyint", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Game", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Office",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    No = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Office", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ticket",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TicketNo = table.Column<string>(type: "varchar(64)", unicode: false, maxLength: 64, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OfficeId = table.Column<int>(type: "int", nullable: false),
                    GameId = table.Column<int>(type: "int", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ticket", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ticket_Game_GameId",
                        column: x => x.GameId,
                        principalTable: "Game",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ticket_Office_OfficeId",
                        column: x => x.OfficeId,
                        principalTable: "Office",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tip",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TicketId = table.Column<int>(type: "int", nullable: false),
                    No1 = table.Column<byte>(type: "tinyint", nullable: false),
                    No2 = table.Column<byte>(type: "tinyint", nullable: false),
                    No3 = table.Column<byte>(type: "tinyint", nullable: false),
                    No4 = table.Column<byte>(type: "tinyint", nullable: false),
                    No5 = table.Column<byte>(type: "tinyint", nullable: false),
                    No6 = table.Column<byte>(type: "tinyint", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tip", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tip_Ticket_TicketId",
                        column: x => x.TicketId,
                        principalTable: "Ticket",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Office_Name",
                table: "Office",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_GameId",
                table: "Ticket",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_OfficeId",
                table: "Ticket",
                column: "OfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_TicketNo",
                table: "Ticket",
                column: "TicketNo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tip_TicketId",
                table: "Tip",
                column: "TicketId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tip");

            migrationBuilder.DropTable(
                name: "Ticket");

            migrationBuilder.DropTable(
                name: "Game");

            migrationBuilder.DropTable(
                name: "Office");
        }
    }
}
