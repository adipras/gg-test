using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace gg_test.Migrations
{
    /// <inheritdoc />
    public partial class UpdateKPIModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "KPIs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    MonthlyRevenue = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    NetProfit = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    ProfitMargin = table.Column<decimal>(type: "decimal(65,30)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KPIs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KPIs_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Company A" },
                    { 2, "Company B" }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$45lIcvCeDBbZElA79B3kYe1hyHimb5.AgAUvzmIkYTfqpuCx6ocnm");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "Password",
                value: "$2a$11$x1vtGo3gFS4Wm5T/fRuQnexwywVd.ogk0RU.wUB2rdBB4/EOMUxN6");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "Password",
                value: "$2a$11$Ely1uPtNsH7bzi/q6LRWPO53LhcDOr7C3aUtHyToofV5pRjB71Kj.");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                column: "Password",
                value: "$2a$11$khFETW3SimDiMibnsBGI5O8g9RHJZ1UTiBzWCzwJkcwzJ9sykOhxS");

            migrationBuilder.InsertData(
                table: "KPIs",
                columns: new[] { "Id", "CompanyId", "MonthlyRevenue", "NetProfit", "ProfitMargin" },
                values: new object[,]
                {
                    { 1, 1, 100000m, 20000m, 20m },
                    { 2, 1, 120000m, 25000m, 21m },
                    { 3, 2, 150000m, 30000m, 20m },
                    { 4, 2, 160000m, 32000m, 20m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_KPIs_CompanyId",
                table: "KPIs",
                column: "CompanyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KPIs");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$l8h0N0aGnP8T/J.KtrNEaOCOUwHDrift/szAam3rq7qC03YM92I9y");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "Password",
                value: "$2a$11$.ZweTpP7amR5CtSgmCI5/udyyE52ajpcfl0e1fiH8aX27JLKFOpge");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "Password",
                value: "$2a$11$V3XMmUuKGiOuwx48uda6cu7Lz5WfzqJ.qDWq9MZYNZAaAtnXA9sq2");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                column: "Password",
                value: "$2a$11$IU6Tq.t2jgcCyhvkgLhnsugbez12dQNT7x1WfUe7D91Ks1zU1R1q6");
        }
    }
}
