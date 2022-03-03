using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeConsole.ExchangeData;

namespace TradeConsole.ExchangeOps
{
    public class BinanceMaintainence
    {
        BinanceOps _binanceOps = new BinanceOps();
        // Methods to update Database for UI
        public async Task UpdateBalances()
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
                using(TradingContext tradingContext = new())
                {
                    tradingContext.AddRange(nonZeroBalances);
                    await tradingContext.SaveChangesAsync();
                }
            }
            else
            {
                Console.WriteLine("No response received");
                
            }
        }


    }
}
