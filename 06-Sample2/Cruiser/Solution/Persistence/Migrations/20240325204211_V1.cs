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
                name: "ShippingCompany",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    City = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    PLZ = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: true),
                    Street = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    StreetNo = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShippingCompany", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CruiseShip",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(256)", unicode: false, maxLength: 256, nullable: false),
                    YearOfConstruction = table.Column<long>(type: "bigint", nullable: false),
                    Tonnage = table.Column<long>(type: "bigint", nullable: true),
                    Length = table.Column<decimal>(type: "decimal(6,2)", nullable: true),
                    Cabins = table.Column<long>(type: "bigint", nullable: true),
                    Passengers = table.Column<long>(type: "bigint", nullable: true),
                    Crew = table.Column<long>(type: "bigint", nullable: true),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShippingCompanyId = table.Column<int>(type: "int", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CruiseShip", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CruiseShip_ShippingCompany_ShippingCompanyId",
                        column: x => x.ShippingCompanyId,
                        principalTable: "ShippingCompany",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ShipName",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    CruiseShipId = table.Column<int>(type: "int", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShipName", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShipName_CruiseShip_CruiseShipId",
                        column: x => x.CruiseShipId,
                        principalTable: "CruiseShip",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CruiseShip_Name",
                table: "CruiseShip",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CruiseShip_ShippingCompanyId",
                table: "CruiseShip",
                column: "ShippingCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_ShipName_CruiseShipId",
                table: "ShipName",
                column: "CruiseShipId");

            migrationBuilder.CreateIndex(
                name: "IX_ShipName_Id_Name",
                table: "ShipName",
                columns: new[] { "Id", "Name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ShippingCompany_Name",
                table: "ShippingCompany",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShipName");

            migrationBuilder.DropTable(
                name: "CruiseShip");

            migrationBuilder.DropTable(
                name: "ShippingCompany");
        }
    }
}
