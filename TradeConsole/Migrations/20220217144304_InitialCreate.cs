using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TradeConsole.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BinanceAccountInfo",
                columns: table => new
                {
                    AccountInfoResponseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    makerCommission = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    takerCommission = table.Column<int>(type: "int", nullable: false),
                    buyerCommiss = table.Column<int>(type: "int", nullable: false),
                    sellerCommiss = table.Column<int>(type: "int", nullable: false),
                    canTrade = table.Column<bool>(type: "bit", nullable: false),
                    canWithdraw = table.Column<bool>(type: "bit", nullable: false),
                    canDeposit = table.Column<bool>(type: "bit", nullable: false),
                    updateTime = table.Column<long>(type: "bigint", nullable: false),
                    accountType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BinanceAccountInfo", x => x.AccountInfoResponseId);
                });

            migrationBuilder.CreateTable(
                name: "BinanceTradeHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Symbol = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TradeId = table.Column<long>(type: "bigint", nullable: false),
                    OrderId = table.Column<long>(type: "bigint", nullable: false),
                    OrderListId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QuoteQty = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Commission = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CommissionAsset = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TimeStamp = table.Column<long>(type: "bigint", nullable: false),
                    isBuyer = table.Column<bool>(type: "bit", nullable: false),
                    isMaker = table.Column<bool>(type: "bit", nullable: false),
                    isBestMatch = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BinanceTradeHistory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BinanceBalances",
                columns: table => new
                {
                    BalancesId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Asset = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Balance = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Locked = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountInfoResponseId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BinanceBalances", x => x.BalancesId);
                    table.ForeignKey(
                        name: "FK_BinanceBalances_BinanceAccountInfo_AccountInfoResponseId",
                        column: x => x.AccountInfoResponseId,
                        principalTable: "BinanceAccountInfo",
                        principalColumn: "AccountInfoResponseId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BinanceBalances_AccountInfoResponseId",
                table: "BinanceBalances",
                column: "AccountInfoResponseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BinanceBalances");

            migrationBuilder.DropTable(
                name: "BinanceTradeHistory");

            migrationBuilder.DropTable(
                name: "BinanceAccountInfo");
        }
    }
}
