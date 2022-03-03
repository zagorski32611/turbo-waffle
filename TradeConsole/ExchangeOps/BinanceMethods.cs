using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeConsole.ExchangeData;
using TradeConsole.ExchangeOps;
using Newtonsoft.Json;


namespace TradeConsole.ExchangeOps
{
    public class BinanceMethods
    {
        BinanceOps _binanceOps = new BinanceOps();
        public async Task<List<BinanceObjects.TradeHistoryEntry>> TradeHistory(string pair)
        {
            List<BinanceObjects.TradeHistoryEntry> tradeHistories = new();
            string tradeResponse = await _binanceOps.CallBinance("myTrades", HttpMethod.Get, $"symbol={pair}");
            var tradeResp = JsonConvert.DeserializeObject<List<BinanceObjects.TradeHistoryEntry>>(tradeResponse);

            if (tradeResp is not null)
            {
                foreach (var trade in tradeResp)
                {
                    tradeHistories.Add(trade);
                }
                return tradeHistories;
            }
            else
            {
                return new List<BinanceObjects.TradeHistoryEntry>();
            }
        }

        public async Task UpdateBalances()
        {
            string balances = await _binanceOps.CallBinance("account", HttpMethod.Get);
            List<BinanceObjects.Balances> balresponse = JsonConvert.DeserializeObject<List<BinanceObjects.Balances>>(balances);
            if (balresponse is not null)
            {
                foreach (var bal in balresponse)
                {
                    Console.WriteLine(bal.Asset + bal.Balance);

                }
                using (TradingContext tradingContext = new())
                {
                    tradingContext.Add(balresponse);
                    await tradingContext.SaveChangesAsync();
                }

            }
        }

        public async Task<BinanceObjects.DailyPriceHistory> GetDailyPrice(string asset)
        {
            BinanceObjects.DailyPriceHistory dailyPriceHistory = new();
            string dailyPriceUrl = "ticker/24hr";
            string query = $"symbol={asset}";
            string price = await _binanceOps.CallBinanceNoAuth(dailyPriceUrl, query);
            dailyPriceHistory = JsonConvert.DeserializeObject<BinanceObjects.DailyPriceHistory>(price);
            return dailyPriceHistory;

        }
        
        public async Task<List<BinanceObjects.Balances>> GetBalances(bool includeZero = false)
        {
            string response = await _binanceOps.CallBinance("account", HttpMethod.Get);
            var binanceAccount = JsonConvert.DeserializeObject<BinanceObjects.AccountInfoResponse>(response);
            List<BinanceObjects.Balances> balances = binanceAccount.Balances;


            List<BinanceObjects.Balances> nonZeroBalances = new();

            if (response is not null && balances.Count > 0)
            {
                foreach (BinanceObjects.Balances balance in balances)
                {
                    if (Convert.ToDecimal(balance.Balance) > 0)
                    {
                        nonZeroBalances.Add(balance);
                    }
                }
                return nonZeroBalances;
            }
            else
            {
                Console.WriteLine("No response received");
                return balances;
            }
        }

        public string GetSpecificBalance(List<BinanceObjects.Balances> balances, string asset)
        {
            if (asset.Contains("USD"))
                asset = asset.Substring(0, asset.Length - 3);
            else if (asset.Contains("BUSD"))
                asset = asset.Substring(0, asset.Length - 4);

            var bal = balances.Find(x => x.Asset == asset);

            return bal is null ? "" :  bal.Balance;
            
        }

        public BinanceObjects.TradeHistoryEntry GetLastTradeEntry(List<BinanceObjects.TradeHistoryEntry> tradeHistories)
        {
            var lastTrade = (from trade in tradeHistories
                             where trade.isBuyer == true
                             orderby trade.TimeStamp descending
                             select trade)
                   .FirstOrDefault();
            return lastTrade;

        }
    }
}