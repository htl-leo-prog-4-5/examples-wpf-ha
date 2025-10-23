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
                name: "City",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_City", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Infrastructure",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    City = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    PLZ = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: true),
                    Street = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    StreetNo = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Infrastructure", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Line",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Length = table.Column<decimal>(type: "decimal(9,3)", nullable: true),
                    IsElectric = table.Column<bool>(type: "bit", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Line", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RailwayCompany",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    City = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    PLZ = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: true),
                    Street = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    StreetNo = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RailwayCompany", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Station",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    StateCode = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    IsRegional = table.Column<bool>(type: "bit", nullable: false),
                    IsExpress = table.Column<bool>(type: "bit", nullable: false),
                    IsIntercity = table.Column<bool>(type: "bit", nullable: false),
                    Remark = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    CityId = table.Column<int>(type: "int", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Station", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Station_City_CityId",
                        column: x => x.CityId,
                        principalTable: "City",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InfrastructureStation",
                columns: table => new
                {
                    InfrastructuresId = table.Column<int>(type: "int", nullable: false),
                    StationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InfrastructureStation", x => new { x.InfrastructuresId, x.StationId });
                    table.ForeignKey(
                        name: "FK_InfrastructureStation_Infrastructure_InfrastructuresId",
                        column: x => x.InfrastructuresId,
                        principalTable: "Infrastructure",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InfrastructureStation_Station_StationId",
                        column: x => x.StationId,
                        principalTable: "Station",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LineStation",
                columns: table => new
                {
                    LinesId = table.Column<int>(type: "int", nullable: false),
                    StationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LineStation", x => new { x.LinesId, x.StationId });
                    table.ForeignKey(
                        name: "FK_LineStation_Line_LinesId",
                        column: x => x.LinesId,
                        principalTable: "Line",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LineStation_Station_StationId",
                        column: x => x.StationId,
                        principalTable: "Station",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RailwayCompanyStation",
                columns: table => new
                {
                    RailwayCompaniesId = table.Column<int>(type: "int", nullable: false),
                    StationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RailwayCompanyStation", x => new { x.RailwayCompaniesId, x.StationId });
                    table.ForeignKey(
                        name: "FK_RailwayCompanyStation_RailwayCompany_RailwayCompaniesId",
                        column: x => x.RailwayCompaniesId,
                        principalTable: "RailwayCompany",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RailwayCompanyStation_Station_StationId",
                        column: x => x.StationId,
                        principalTable: "Station",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_City_Name",
                table: "City",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Infrastructure_Code",
                table: "Infrastructure",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Infrastructure_Name",
                table: "Infrastructure",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InfrastructureStation_StationId",
                table: "InfrastructureStation",
                column: "StationId");

            migrationBuilder.CreateIndex(
                name: "IX_Line_Name",
                table: "Line",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LineStation_StationId",
                table: "LineStation",
                column: "StationId");

            migrationBuilder.CreateIndex(
                name: "IX_RailwayCompany_Code",
                table: "RailwayCompany",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RailwayCompany_Name",
                table: "RailwayCompany",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RailwayCompanyStation_StationId",
                table: "RailwayCompanyStation",
                column: "StationId");

            migrationBuilder.CreateIndex(
                name: "IX_Station_CityId",
                table: "Station",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Station_Code",
                table: "Station",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Station_Name",
                table: "Station",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InfrastructureStation");

            migrationBuilder.DropTable(
                name: "LineStation");

            migrationBuilder.DropTable(
                name: "RailwayCompanyStation");

            migrationBuilder.DropTable(
                name: "Infrastructure");

            migrationBuilder.DropTable(
                name: "Line");

            migrationBuilder.DropTable(
                name: "RailwayCompany");

            migrationBuilder.DropTable(
                name: "Station");

            migrationBuilder.DropTable(
                name: "City");
        }
    }
}
