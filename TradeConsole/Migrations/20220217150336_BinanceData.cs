using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TradeConsole.Migrations
{
    public partial class BinanceData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "binanceDatas",
                columns: table => new
                {
                    BinanceDataId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BinanceAPIKey = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BinanceAPISecret = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_binanceDatas", x => x.BinanceDataId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "binanceDatas");
        }
    }
}
