using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace TradeConsole.ExchangeData
{
    public class BinanceData
    {
        public int BinanceDataId { get; set; }
        public string BinanceAPIKey { get; set; }
        public string BinanceAPISecret { get; set; }
    }

}
