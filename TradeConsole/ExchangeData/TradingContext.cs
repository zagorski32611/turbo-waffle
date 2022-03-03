using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Configuration;

namespace TradeConsole.ExchangeData
{
    public class TradingContext : DbContext
    {
        public DbSet<BinanceObjects.Balances> BinanceBalances { get; set; }
        public DbSet<BinanceObjects.AccountInfoResponse> BinanceAccountInfo { get; set; }
        public DbSet<BinanceObjects.TradeHistoryEntry> BinanceTradeHistory { get; set; }

        public DbSet<BinanceData> binanceDatas { get; set; }

        public string DbPath { get; set; }

        public TradingContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = System.IO.Path.Join(path, "tradingconsole.db");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlServer("Data Source=localhost;Initial Catalog=tradingconsole.db;Integrated Security=SSPI");

    }
}
