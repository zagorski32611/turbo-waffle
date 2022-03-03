using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeConsole.ExchangeData
{
    public class BinanceObjects
    {
        public class AccountInfoResponse
        {
            public int AccountInfoResponseId { get; set; }
            public string makerCommission { get; set; }
            public int takerCommission { get; set; }
            public int buyerCommiss { get; set; }
            public int sellerCommiss { get; set; }
            public bool canTrade { get; set; }
            public bool canWithdraw { get; set; }
            public bool canDeposit { get; set; }
            public long updateTime { get; set; }
            public string accountType { get; set; }
            public List<Balances> Balances { get; set; }

        }
        public class Balances
        {
            public int BalancesId { get; set; }
            public string Asset { get; set; }

            [JsonProperty("free")]
            public string Balance { get; set; }
            public string Locked { get; set; }
        }

        public class TradeHistoryEntry
        {
            public int Id { get; set; }

            [JsonProperty("symbol")]
            public string Symbol { get; set; }

            [JsonProperty("id")]
            public long TradeId { get; set; }

            [JsonProperty("orderId")]
            public long OrderId { get; set; }
            public int OrderListId { get; set; }

            [JsonProperty("price")]
            public string Price { get; set; }

            [JsonProperty("qty")]
            public string Quantity { get; set; }
            public string QuoteQty { get; set; }
            public string Commission { get; set; }
            public string CommissionAsset { get; set; }

            [JsonProperty("time")]
            public long TimeStamp { get; set; }
            public bool isBuyer { get; set; }
            public bool isMaker { get; set; }
            public bool isBestMatch { get; set; }
        }

        public class DailyPriceHistory
        {
            public string symbol { get; set; }
            public string priceChange { get; set; }
            public string priceChangePercent { get; set; }
            public string weightedAvgPrice { get; set; }
            public string prevClosePrice { get; set; }
            public string lastPrice { get; set; }
            public string lastQty { get; set; }
            public string bidPrice { get; set; }
            public string bidQty { get; set; }
            public string askPrice { get; set; }
            public string askQty { get; set; }
            public string openPrice { get; set; }
            public string highPrice { get; set; }
            public string lowPrice { get; set; }
            public string volume { get; set; }
            public string quoteVolume { get; set; }
            public long openTime { get; set; }
            public long closeTime { get; set; }

            public int firstId { get; set; }
            public int lastId { get; set; }
            public int count { get; set; }

        }

    }
}
/*
     "symbol": "BNBBTC",
    "id": 28457,
    "orderId": 100234,
    "orderListId": -1,
    "price": "4.00000100",
    "qty": "12.00000000",
    "quoteQty": "48.000012",
    "commission": "10.10000000",
    "commissionAsset": "BNB",
    "time": 1499865549590,
    "isBuyer": true,
    "isMaker": false,
    "isBestMatch": true
*/
