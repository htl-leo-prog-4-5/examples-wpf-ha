using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class V2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Office",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PLZ",
                table: "Office",
                type: "nvarchar(16)",
                maxLength: 16,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StateId",
                table: "Office",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Street",
                table: "Office",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StreetNo",
                table: "Office",
                type: "nvarchar(16)",
                maxLength: 16,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Count3",
                table: "Game",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Count4",
                table: "Game",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Count5",
                table: "Game",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Count5ZZ",
                table: "Game",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Count6",
                table: "Game",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MaxNo",
                table: "Game",
                type: "int",
                nullable: false,
                defaultValue: 45);

            migrationBuilder.CreateTable(
                name: "State",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_State", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Office_StateId",
                table: "Office",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_State_Name",
                table: "State",
                column: "Name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Office_State_StateId",
                table: "Office",
                column: "StateId",
                principalTable: "State",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Office_State_StateId",
                table: "Office");

            migrationBuilder.DropTable(
                name: "State");

            migrationBuilder.DropIndex(
                name: "IX_Office_StateId",
                table: "Office");

            migrationBuilder.DropColumn(
                name: "City",
                table: "Office");

            migrationBuilder.DropColumn(
                name: "PLZ",
                table: "Office");

            migrationBuilder.DropColumn(
                name: "StateId",
                table: "Office");

            migrationBuilder.DropColumn(
                name: "Street",
                table: "Office");

            migrationBuilder.DropColumn(
                name: "StreetNo",
                table: "Office");

            migrationBuilder.DropColumn(
                name: "Count3",
                table: "Game");

            migrationBuilder.DropColumn(
                name: "Count4",
                table: "Game");

            migrationBuilder.DropColumn(
                name: "Count5",
                table: "Game");

            migrationBuilder.DropColumn(
                name: "Count5ZZ",
                table: "Game");

            migrationBuilder.DropColumn(
                name: "Count6",
                table: "Game");

            migrationBuilder.DropColumn(
                name: "MaxNo",
                table: "Game");
        }
    }
}
