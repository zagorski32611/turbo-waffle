using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeConsole.ExchangeData;
using TradeConsole.ExchangeOps;
using Spectre.Console;


namespace TradeConsole.UI
{
    public class ConsoleUI
    {
        BinanceMethods _binanceMethods = new();

        public void StartUpPage()
        {
            Markup markup = new Markup("[bold]Welcome![/]");
            // get balance, then get market value


        }

        public void AccountBalanceTable(List<BinanceObjects.Balances> balances1)
        {
            var OrderBal = balances1.OrderByDescending(b => b.Balance);
            Table balTable = new Table().Centered();
            balTable.AddColumns("Asset", "Balance", "Locked");
            foreach (var balance in OrderBal)
            {
                
                balTable.AddRow(balance.Asset, balance.Balance, balance.Locked);
            }
            AnsiConsole.Write(balTable);
        }

        public async Task PositionInfo(string asset, string specificBal)
        {
            BinanceObjects.DailyPriceHistory dailyPrice = await _binanceMethods.GetDailyPrice(asset);
            List<BinanceObjects.TradeHistoryEntry> tradeHistories = await _binanceMethods.TradeHistory(asset);
            BinanceObjects.TradeHistoryEntry lastTrade = _binanceMethods.GetLastTradeEntry(tradeHistories);

            decimal profitPerCoin = Convert.ToDecimal(dailyPrice.lastPrice) - Convert.ToDecimal(lastTrade.Price);
            decimal positionProfit = profitPerCoin * Convert.ToDecimal(lastTrade.Quantity);
            decimal totalPurchase = Convert.ToDecimal(lastTrade.Price) * Convert.ToDecimal(lastTrade.Quantity);
            decimal totalSale = Convert.ToDecimal(dailyPrice.bidPrice) * Convert.ToDecimal(specificBal);
            string textColor = Convert.ToDecimal(dailyPrice.priceChange) >= (decimal)0.00 ? "green" : "red";
            string positionColor = positionProfit >= 0 ? "green" : "red";

            //decimal marketValue = Convert.ToDecimal()
            
            Table pricetable = new Table().Centered();
            pricetable.Title("Market Data");
            pricetable.AddColumns("Symbol","Current Price", "24H Price Change", "24H High", "24H Low");
            pricetable.AddRow($"{dailyPrice.symbol}", $"{dailyPrice.lastPrice}", $"[{textColor}]{dailyPrice.priceChange} ({dailyPrice.priceChangePercent}%)[/]", $"{dailyPrice.highPrice}", $"{dailyPrice.lowPrice}");

            Table tradeTable = new Table().Centered();
            tradeTable.Title("TradeTable");
            tradeTable.AddColumns("Date", "Quantity", "Buy Price", "Sell Price (Market Bid)", "Total Purchase", "Total Sale", "Profit", "Profit Per Coin");
            tradeTable.AddRow($"{DateTimeOffset.FromUnixTimeMilliseconds(lastTrade.TimeStamp)}", $"{lastTrade.Quantity}", $"{lastTrade.Price}", $"{dailyPrice.bidPrice}",$"{totalPurchase}", $"{totalSale}", $"[{positionColor}]{positionProfit}[/]", $"[{positionColor}]{profitPerCoin}[/]");

            AnsiConsole.Write(pricetable);
            AnsiConsole.Write(tradeTable);
        }

        public async Task<BinanceObjects.DailyPriceHistory> GetDailyPrice(string asset)
        {
            BinanceObjects.DailyPriceHistory dailyPrice = await _binanceMethods.GetDailyPrice(asset);
            return dailyPrice;
        }

        public async Task<List<BinanceObjects.TradeHistoryEntry>> GetTradeHistoryEntries(string asset)
        {
            List<BinanceObjects.TradeHistoryEntry> tradeHistories = await _binanceMethods.TradeHistory(asset);
            
            return tradeHistories;
        }

        public async Task GetTradeHistoryTable(string asset, string currentBalance)
        {
            List<BinanceObjects.TradeHistoryEntry> tradeHistory = await _binanceMethods.TradeHistory(asset);
            var orderedHistory = tradeHistory.OrderByDescending(o => o.TimeStamp);

            Table table = new Table();
            table.AddColumns("date", "asset", "price", "quantity","total cost", "action");

            foreach(var trade in orderedHistory)
            {
                var normalTime = DateTimeOffset.FromUnixTimeMilliseconds(trade.TimeStamp).ToLocalTime();
                normalTime = normalTime.LocalDateTime;
                string buySell = trade.isBuyer == true ? "Buy" : "Sell";

                table.AddRow($"{normalTime}", $"{trade.Symbol}", $"{trade.Price}", $"{trade.Quantity}", $"{buySell}");
            }
            AnsiConsole.Write(table);
        }

        public async Task GetPositionInfoTable(string asset, string specificBalance)
        {
            List<BinanceObjects.TradeHistoryEntry> tradeHistories = await _binanceMethods.TradeHistory(asset);
            BinanceObjects.TradeHistoryEntry lastTrade = _binanceMethods.GetLastTradeEntry(tradeHistories);
            var normalTime = DateTimeOffset.FromUnixTimeMilliseconds(lastTrade.TimeStamp);
            Table tradeHistoryTable = new Table().Centered();
            tradeHistoryTable.Title("Position Data");
            tradeHistoryTable.AddColumns("Date", "Purchase Price", "Quantity", "Total", "Current Market Value");
            tradeHistoryTable.AddRow($"{normalTime}", $"{lastTrade.Price}", $"{lastTrade.Quantity}", $"{Convert.ToDecimal(lastTrade.Price) * Convert.ToDecimal(lastTrade.Quantity)}", $"{lastTrade.Commission}");
            AnsiConsole.Write(tradeHistoryTable);
        }
    }
}
